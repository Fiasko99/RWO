// Иницилизация сервера
const express = require('express')
const app = express()
const bodyParser = require('body-parser')
const path = require('path')
const multer = require('multer')
const chalk = require('chalk')
const { v4 } = require('uuid')
const cors = require('cors')
const nodemailer = require("nodemailer");
const fs = require('fs')

// Кастомные импорты
const conSeq = require("./conSeq")
const {
  compositions, rating,
  age_limits, readers,
  offers, writters,
  sponsorship
} = require('./modules/db')
const { mkdir } = require('fs')
let PORT = process.env.PORT || 3001;
let uri = `http://176.100.0.104:${PORT}`

// if (process.env.PORT || PORT) {
//   // Корректная работа режима HTML5 history
//   app.use(history());
// }

// Настройка cors
app.use(cors());

// Парсинг json - application/json
app.use(bodyParser.json())

// Парсинг запросов по типу: application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: true }));
app.use(express.static('app'))
// Покдлючение к базе данных
const sequelize = conSeq()

// Стэк модулей для базы данных
const Readers = sequelize.define('readers', readers)
const Writters = sequelize.define('writters', writters)
const Offers = sequelize.define('offers', offers)
const Compositions = sequelize.define('compositions', compositions)
const Rating = sequelize.define('rating', rating)
const Sponsorship = sequelize.define('sponsorship', sponsorship)
const AgeLimits = sequelize.define('age_limits', age_limits)

associationsDB()

// Запросы
app.get('/', (req, res) => {
  res.send('<a href="http://176.100.0.104:3001/Debug.rar" targer="_blank">Скачать файл</a>')
})

app.post('/api/login', async (req, res) => {
  if (req.body.Role == 'Читатель') {
    let response = await checkReader(
      req.body.Login, req.body.Password
    )
    return res.end(res.fatal != true ? res.end(JSON.stringify(response)) : "error")
  }
  else if (req.body.Role == 'Писатель') {
    let response = await checkWriter(
      req.body.Login, req.body.Password
    )
    return res.end(res.fatal != true ? res.end(JSON.stringify(response)) : "error")
  }
  else if (req.body.Role == 'Инвестор') {
    let response = await checkOffer(
      req.body.Login, req.body.Password
    )
    return res.end(res.fatal != true ? res.end(JSON.stringify(response)) : "error")
  }
  return res.end('not found')
})
 // attr -> id_user && role
let NotComfirmUsers = [
  {
    id_user: -1,
    role: '-1'
  }
]


fileHandler()
function fileHandler(){

  fs.readFile('NotConfirmUsers.txt', 'utf8', (err, data) => {
      if(err) throw err;
      console.log('--------- [File Data] ---------')
      console.log(NotComfirmUsers)
      console.log(data)
      console.log('--------- [File Data] ---------')
  })

}




app.get('/api/confirm/:id', (req, res) => {
  if (NotComfirmUsers.findIndex(user => user.id == req.params.id)) {
    NotComfirmUsers = NotComfirmUsers.filter(user => user.id_user != req.params.id)
    console.log(NotComfirmUsers)
    return res.sendFile(__dirname + '/index.html')
  }
})

app.post('/api/registration', async (req, res) => {
  console.log('some')
  if (req.body.Role == 'Читатель') {
    await Readers.create({
      login: req.body.Login,
      password: req.body.Password,
      name: req.body.Name,
      surname: req.body.Surname,
      email: req.body.Email,
      secret_code: req.body.SecretCode,
    }).then(async result => {
      let reader = {
        id_user: result.id,
        role: 'reader'
      }
      NotComfirmUsers.push(reader)
      let IsMail = await mailSend(
        result.id,
        uri,
        result.email
      )
      if (IsMail) {
        console.log(IsMail)
        return res.end(
          JSON.stringify(result)
        )
      }
      return res.end('error mail')
    }).catch(() => {
      return res.end('error database')
    })
  }
  else if (req.body.Role == 'Писатель') {
    await Writters.create({
      login: req.body.Login,
      password: req.body.Password,
      name: req.body.Name,
      surname: req.body.Surname,
      email: req.body.Email,
      secret_code: req.body.SecretCode,
    }).then(async result => {
      let writter = {
        id_user: result.id,
        role: 'writter'
      }
      NotComfirmUsers.push(writter)
      let IsMail = await mailSend(
        result.id,
        uri,
        result.email
      )
      if (IsMail) {
        console.log(IsMail)
        return res.end(
          JSON.stringify(result)
        )
      }
      return res.end('error mail')
    }).catch(() => {
      return res.end('error database')
    })
  }
  else if (req.body.Role == 'Инвестор') {
    await Offers.create({
      login: req.body.Login,
      password: req.body.Password,
      name: req.body.Name,
      surname: req.body.Surname,
      email: req.body.Email,
      secret_code: req.body.SecretCode,
    }).then(async result => {
      let offer = {
        id_user: result.id,
        role: 'offer'
      }
      NotComfirmUsers.push(offer)
      console.log(NotComfirmUsers)
      let IsMail = await mailSend(
        result.id,
        uri,
        result.email
      )
      if (IsMail) {
        
        return res.end(
          JSON.stringify(result)
        )
      }
      return res.end('error mail')
    }).catch(() => {
      return res.end('error database')
    })
  }
  return res.end('Fail registration')
})

app.get('/api/books', async (req, res) => {
  let newFormBooks = []
  let books = await Compositions.findAll({
    attributes: [
      'id', 'name_composition', 
      'genre', 'age_limit_id',
      'writter_id',
    ]
  }).catch(() => {
    return res.end('Fail')
  })
  for (let book of books) {
    let writter = await Writters.findOne({
      where: {
        id: book.writter_id
      },
      attributes: [
        'name', 'surname'
      ]
    })
    let age_limit = await AgeLimits.findOne({
      where: {
        id: book.age_limit_id
      },
      attributes: [
        'value'
      ]
    })
    let newFormBook = {
      id: book.id,
      name_composition: book.name_composition,
      genre: book.genre,
      writter_name: writter.name + " " + writter.surname,
      age_limit_value: age_limit.value.toString()
    }
    newFormBooks.push(newFormBook)
  }
  if(newFormBooks.length > 0) {
    return res.end(
      JSON.stringify(newFormBooks)
    )
  }
  return res.end('not found')
})

app.get('/api/writters/list', async (req, res) => {
  await Writters.findAll({
    attributes: [
      'id',
      'name', 'surname',
      'work_experience'
    ]
  }).then(response => {
    return res.end(
      JSON.stringify(response)
    )
  })
})

// Запуск сервера 
app
  .listen(PORT, async () => {
    await sequelize.sync({ alter: true })
    console.log(chalk.green(`[server] connection DB `));
    console.log(chalk.green(`[server] server start `))
    console.log(chalk.bold.blueBright(`_`.repeat(48)))
});

// Проверка на данные
async function checkReader(login, password) {
  let user = await Readers.findOne({
    where: {
      login: login,
      password: password,
    },
    attributes: [
      'id', 'name',
      'surname', 'email',
      'date_of_born'
    ]
  }).catch(() => {
    return 'error'
  })
  if (user) {
    if (!NotComfirmUsers.findIndex(x => x.id_user == user.id) > 0)
      return 'u r blocked'
    return user
  }
  return 'not found'
}

async function checkWriter(login, password) {
  let user = await Writters.findOne({
    where: {
      login: login,
      password: password,
    },
    attributes: [
      'id', 'name',
      'surname', 'email',
      'work_experience'
    ]
  }).catch(err => {
    return 'error'
  })
  if (user) {
    if (!NotComfirmUsers.findIndex(x => x.id_user == user.id))
      return 'u r blocked'
    return user
  }
  return 'not found'
}

async function checkOffer(login, password) {
  let user = await Offers.findOne({
    where: {
      login: login,
      password: password,
    },
    attributes: [
      'id', 'name',
      'surname', 'email',
      'note'
    ]
  }).catch(() => {
    console.log('error')
    return 'error'
  })
  if (user) {
    console.log(!NotComfirmUsers.findIndex(x => x.id_user != user.id))
    if (!NotComfirmUsers.findIndex(x => x.id_user != user.id))
      return user
    return 'u r blocked'
  }
  return 'not found'
}

// Ассоциации Базы Данных
function associationsDB() {
  Compositions.belongsTo(AgeLimits,{
    foreignKey: {
      name: 'age_limit_id',
      allowNull: false
    },
    as: 'age_limit'
  })
  AgeLimits.hasMany(Compositions, {
    foreignKey: 'age_limit_id',
    as: 'compositions'
  })

  Compositions.belongsTo(Writters, {
    foreignKey: {
      name: 'writter_id',
      allowNull: false
    },
    as: 'writter'
  })
  Writters.hasMany(Compositions, {
    foreignKey: 'writter_id',
    as: 'compositions'
  })

  Sponsorship.belongsTo(Writters, {
    foreignKey: {
      name: 'writter_id',
      allowNull: false
    },
    as: 'writter'
  })
  Writters.hasMany(Sponsorship, {
    foreignKey: 'writter_id',
    as: 'Sponsorship'
  })
  Sponsorship.belongsTo(Offers, {
    foreignKey: {
      name: 'offer_id',
      allowNull: false
    },
    as: 'offers'
  })
  Offers.hasMany(Sponsorship, {
    foreignKey: 'offer_id',
    as: 'Sponsorship'
  })

  Rating.belongsTo(Readers, {
    foreignKey: {
      name: 'reader_id',
      allowNull: false
    },
    as: 'readers'
  })
  Readers.hasMany(Rating, {
    foreignKey: 'reader_id',
    as: 'rating'
  })
  Rating.belongsTo(Compositions, {
    foreignKey: {
      name: 'composition_id',
      allowNull: false
    },
    as: 'compositions'
  })
  Compositions.hasMany(Rating, {
    foreignKey: 'composition_id',
    as: 'rating'
  })
}

async function validateWriter(login, password) {
  let user = await Writters.findOne({
    where: {
      login_writer: login,
      password_writer: password
    }
  })
  return user
}

async function mailSend(id, uri, mail) {
  // Generate test SMTP service account from ethereal.email
  // Only needed if you don't have a real mail account for testing
  // let testAccount = await nodemailer.createTestAccount();

  // create reusable transporter object using the default SMTP transport
  let transporter = nodemailer.createTransport({
    host: "smtp.mail.ru",
    port: 587,
    secure: false, // true for 465, false for other ports
    auth: {
      user: 'yatest2021@mail.ru', // generated ethereal user
      pass: 'Qwerty_1234//', // generated ethereal password
    },
  });

  // send mail with defined transport object
  let info;
  try {
    info = await transporter.sendMail({
      from: '"RWO " <yatest2021@mail.ru>', // sender address
      to: mail, // list of receivers
      subject: "Hello, u r confirm ur profile ✔", // Subject line
      // text: ``, // plain text body
      html: `<a href="${uri}/api/confirm/${id}">Click for open app</a>`, // html body
    })
  } catch (error) {
    return false
  }
 

  console.log("Message sent: %s", info.messageId);
  // Message sent: <b658f8ca-6296-ccf4-8306-87d57a0b4321@example.com>

  // Preview only available when sending through an Ethereal account
  console.log("Preview URL: %s", nodemailer.getTestMessageUrl(info));
  // Preview URL: https://ethereal.email/message/WaQKMgKddxQDoou...
  if (info.messageId) {
    return true
  }
  return false
}

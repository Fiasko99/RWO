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
  sponsorship,
  languages,
  genres
} = require('./modules/db')
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
const Languages = sequelize.define('languages', languages)
const Genres = sequelize.define('genres', genres)
const Readers = sequelize.define('readers', readers)
const Writters = sequelize.define('writters', writters)
const Offers = sequelize.define('offers', offers)
const Compositions = sequelize.define('compositions', compositions)
const Rating = sequelize.define('rating', rating)
const Sponsorship = sequelize.define('sponsorship', sponsorship)
const AgeLimits = sequelize.define('age_limits', age_limits)

associationsDB()

writeToFileLog('Сервер стартовал в ' + new Date() + '\n\n')

// Запросы
app.get('/', (req, res) => {
  res.send('<a href="http://176.100.0.104:3001/Debug.rar" targer="_blank">Скачать файл</a>')
})

app.post('/api/login', async (req, res) => {
  writeToFileLog(`
                  Пользователь 
                  id: 
                  Инициалы: ${req.body.Login} 
                  запросил вход на клиенте в ${new Date()}
                `)
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


app.get('/api/confirm/:role/:id', async (req, res) => {
  let user
  if (req.params.role == 'Инвестор') {
    user = await Offers.findOne({
      where: {
        id: req.params.id
      }
    }).catch(() => {
      return res.send('Ошибка базы данных')
    })
  } else if (req.params.role == 'Читатель') {
    user = await Readers.findOne({
      where: {
        id: req.params.id
      }
    }).catch(() => {
      return res.send('Ошибка базы данных')
    })
  } else if (req.params.role == 'Писатель') {
    user = await Writters.findOne({
      where: {
        id: req.params.id
      }
    }).catch(() => {
      return res.send('Ошибка базы данных')
    })
  } else {
    return res.send('Ссылка недействительна')
  }
  user.confirm = true
  user.save()
  return res.sendFile(__dirname + '/index.html')
})

app.post('/api/registration', async (req, res) => {
  writeToFileLog(`
                  Пользователь 
                  id: ${req.body.Login}
                  Инициалы: ${req.body.surname} ${req.body.name} 
                  запросил регистрацию на клиенте в ${new Date()}
                `)
  if (req.body.Role == 'Читатель') {
    let user = await Readers.create({
      login: req.body.Login,
      password: req.body.Password,
      name: req.body.Name,
      surname: req.body.Surname,
      email: req.body.Email,
      secret_code: req.body.SecretCode,
    }).catch(() => {
      return res.end('error database')
    })
    let IsMail = await mailSend(
      user.id,
      uri,
      user.email,
      req.body.Role
    )
    if (IsMail) {
      return res.end(
        JSON.stringify(user)
      )
    } else {
      await user.destroy();
      return res.end('error mail')
    }
  }
  else if (req.body.Role == 'Писатель') {
    let user = await Writters.create({
      login: req.body.Login,
      password: req.body.Password,
      name: req.body.Name,
      surname: req.body.Surname,
      email: req.body.Email,
      secret_code: req.body.SecretCode,
    }).catch(() => {
      return res.end('error database')
    })
    let IsMail = await mailSend(
      user.id,
      uri,
      user.email,
      req.body.Role
    )
    if (IsMail) {
      return res.end(
        JSON.stringify(user)
      )
    } else {
      await user.destroy();
      return res.end('error mail')
    }
  }
  else if (req.body.Role == 'Инвестор') {
    let user = await Offers.create({
      login: req.body.Login,
      password: req.body.Password,
      name: req.body.Name,
      surname: req.body.Surname,
      email: req.body.Email,
      secret_code: req.body.SecretCode,
    }).catch(() => {
      return res.end('error database')
    })
    let IsMail = await mailSend(
      user.id,
      uri,
      user.email,
      req.body.Role
    )
    if (IsMail) {
      return res.end(
        JSON.stringify(user)
      )
    } else {
      await user.destroy();
      return res.end('error mail')
    }
  }
  return res.end('Fail registration')
})

app.get('/api/books/:id/:role', async (req, res) => {
  let queryUser;
  console.log(req.params.role)
  if (req.params.role == 'Инвестор') {
    queryUser = await Offers.findOne({
      where: {
        id: req.params.id
      }
    }).catch(() => {
      return res.end('Fail database')
    })
  } else if (req.params.role == 'Читатель') {
    queryUser = await Readers.findOne({
      where: {
        id: req.params.id
      }
    }).catch(() => {
      return res.end('Fail database')
    })
  } else if (req.params.role == 'Писатель') {
    queryUser = await Writters.findOne({
      where: {
        id: req.params.id
      }
    }).catch(() => {
      return res.end('Fail database')
    })
  }
  writeToFileLog(`
                  Пользователь 
                  id: ${queryUser.id}
                  Инициалы: ${queryUser.surname} ${queryUser.name} 
                  запросил список книг на клиенте в ${new Date()}
                `)
  let newFormBooks = []
  let books = await Compositions.findAll({
    attributes: [
      'id', 'name_composition', 
      'genre_id', 'age_limit_id',
      'writter_id', 'language_id'
    ]
  }).catch(() => {
    return res.end('Fail database')
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
    let language = await Languages.findOne({
      where: {
        id: book.language_id
      },
      attributes: [
        'name'
      ]
    })
    let genre = await Genres.findOne({
      where: {
        id: book.genre_id
      },
      attributes: [
        'name'
      ]
    })
    let newFormBook = {
      id: book.id,
      name_composition: book.name_composition,
      genre: genre.name,
      writter_name: writter.name + " " + writter.surname,
      age_limit_value: age_limit.value.toString(),
      language: language.name
    }
    newFormBooks.push(newFormBook)
  }
  if(newFormBooks.length > 0) {
    console.log(newFormBooks)
    return res.end(
      JSON.stringify(newFormBooks)
    )
  }
  return res.end('not found')
})

app.get('/api/writters/list/:id/:role', async (req, res) => {
  let queryUser;
  console.log(req.params.role)
  if (req.params.role == 'Инвестор') {
    queryUser = await Offers.findOne({
      where: {
        id: req.params.id
      }
    }).catch(() => {
      return res.end('Fail database')
    })
  } else if (req.params.role == 'Читатель') {
    queryUser = await Readers.findOne({
      where: {
        id: req.params.id
      }
    }).catch(() => {
      return res.end('Fail database')
    })
  } else if (req.params.role == 'Писатель') {
    queryUser = await Writters.findOne({
      where: {
        id: req.params.id
      }
    }).catch(() => {
      return res.end('Fail database')
    })
  }
  writeToFileLog(`
                  Пользователь 
                  id: ${queryUser.id}
                  Инициалы: ${queryUser.surname} ${queryUser.name} 
                  запросил список писателей на клиенте ${new Date()}
                `)
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

app.get('/api/text/book/:id/:iduser/:role', async (req, res) => {
  let queryUser;
  console.log(req.params.role)
  if (req.params.role == 'Инвестор') {
    queryUser = await Offers.findOne({
      where: {
        id: req.params.iduser
      }
    }).catch(() => {
      return res.end('Fail database')
    })
  } else if (req.params.role == 'Читатель') {
    queryUser = await Readers.findOne({
      where: {
        id: req.params.iduser
      }
    }).catch(() => {
      return res.end('Fail database')
    })
  } else if (req.params.role == 'Писатель') {
    queryUser = await Writters.findOne({
      where: {
        id: req.params.iduser
      }
    }).catch(() => {
      return res.end('Fail database')
    })
  }
  writeToFileLog(`
                  Пользователь 
                  id: ${queryUser.id}
                  Инициалы: ${queryUser.surname} ${queryUser.name} 
                  запросил книгу на клиенте в ${new Date()}
                `)
  await Compositions.findOne({
    where: {
      id: req.params.id
    },
    attributes: [
      'text_composition'
    ]
  }).then(response => {
    return res.end(
      "{}" + response.text_composition
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
      'date_of_born', 'confirm'
    ]
  }).catch(() => {
    return 'error'
  })
  if (user) {
    if (user.confirm != 0) {
      user.confirm = undefined
      return user
    }
    return 'not confirm'
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
      'work_experience', 'confirm'
    ]
  }).catch(() => {
    return 'error'
  })
  if (user) {
    if (user.confirm != 0) {
      user.confirm = undefined
      return user
    }
    return 'not confirm'
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
      'note', 'confirm'
    ]
  }).catch(() => {
    return 'error'
  })
  if (user) {
    if (user.confirm != 0) {
      user.confirm = undefined
      return user
    }
    return 'not confirm'
  }
  return 'not found'
}

// Ассоциации Базы Данных
function associationsDB() {
  Compositions.belongsTo(AgeLimits, {
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

  Compositions.belongsTo(Languages, {
    foreignKey: {
      name: 'id',
      allowNull: false
    },
    as: 'language'
  })
  Languages.hasMany(Compositions, {
    foreignKey: 'language_id',
    as: 'compositions'
  })

  Compositions.belongsTo(Genres, {
    foreignKey: {
      name: 'id',
      allowNull: false
    },
    as: 'genre'
  })
  Genres.hasMany(Compositions, {
    foreignKey: 'genre_id',
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

// отправка письма подтверждения профиля
async function mailSend(id, uri, mail, role) {
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
      html: `<a href="${uri}/api/confirm/${role}/${id}">Click for open app</a>`, // html body
    })
  } catch (error) {
    return false
  }
 

  // Message sent: <b658f8ca-6296-ccf4-8306-87d57a0b4321@example.com>
  // Preview only available when sending through an Ethereal account
  // Preview URL: https://ethereal.email/message/WaQKMgKddxQDoou...
  if (info.messageId) {
    return true
  }
  return false
}

// Ведение журнала
function writeToFileLog(text) {
  fs.appendFile(__dirname + '/Logs.txt', text + "\r\n\n", (err) => {
    console.log('Data add')
    if(err) throw err;
  });
}
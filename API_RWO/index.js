// Иницилизация сервера
const express = require('express')
const app = express()
const bodyParser = require('body-parser')
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
  genres, reports, admins
} = require('./modules/db')
const { resolve } = require('path')
let PORT = process.env.PORT || 3001;
let uri = `http://176.100.0.104:${PORT}`
let errorDB = 'База данных отключена'
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
app.use('/static/reports', express.static('Reports'))
// Покдлючение к базе данных
const sequelize = conSeq()

// Стэк модулей для базы данных
const Languages = sequelize.define('languages', languages)
const Admins = sequelize.define('admins', admins)
const Reports = sequelize.define('reports', reports)
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
  return res.send('<a href="http://176.100.0.104:3001/Debug.rar" targer="_blank">Скачать файл</a>')
})

app.get('/admin/validate/import/:email/:login', async (req, res) => {
  let whatNotUnique = '{}'
  let adminEmail = await Admins.findOne({
    where: {
      email: req.params.email
    }
  })
  if (adminEmail) {
    whatNotUnique += 'email'
  }
  let adminLogin = await Admins.findOne({
    where: {
      login: req.params.login
    }
  })
  if (adminLogin) {
    whatNotUnique += 'login'
  }
  if (whatNotUnique == '{}') {
    whatNotUnique = '{}unique'
  }
  return res.end(whatNotUnique)
})

app.post('/admin/registr/admins', async (req, res) => {
  let newAdmin = Admins.create({
    login: req.body.login,
    email: req.body.email,
    password: req.body.password,
    surname: req.body.surname
  })
  if (newAdmin) {
    return res.end('Админ добавлен')
  }
})

app.get('/api/report/create/writters/:id/:role', async (req, res) => {
  let queryUser;
  if (req.params.role == 'Инвестор') {
    queryUser = await Offers.findOne({
      where: {
        id: req.params.id
      }
    }).catch(() => {
      return res.end('Fail database')
    })
  }
  let writters = await Writters.findAll({
    attributes: [
      'id', 'login', 'name',
      'surname', 'email', 'createdAt',
      'confirm'
    ]
  })
  let path = createReportFile(writters, queryUser, 'Писатель')
  await Reports.create({
    login: queryUser.login,
    link_file: `${uri}/static/reports/${path}`,
  }).catch(() => {
    return res.send('Ошибка базы данных')
  })
  writeToFileLog(`
    Пользователь 
    id: ${queryUser.id}
    Инициалы: ${queryUser.surname} ${queryUser.name} 
    запросил отчёт писателей на клиенте в ${new Date()}
  `)
  return res.end(
    JSON.stringify(writters)
  )
})

app.post('/api/admin/:login', async (req, res) => {
  await Admins.findOne({
    where: {
      login: req.body.Login,
      password: req.body.Password
    }
  }).then(() => {
    return res.end('true')
  })
  return res.end('false')
})

app.post('/api/login', async (req, res) => {
  writeToFileLog(`
    Пользователь 
    id: 
    Инициалы: ${req.body.Login} 
    запросил вход на клиенте в ${new Date()}
  `)
  let response = await validateUser(req.body)
  if (response) {
    if(response.confirm == false) {
      return res.end('Почта не подтверждена')
    } else if(response.duplicate) {
      return res.end('Доступ запрещён')
    } else 
      if (response.confirm) {
        return res.end(JSON.stringify(response))
    }
  }
  return res.end('Неверный логин или пароль')
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
  if (new Date().getHours() - user.createdAt.getHours() <= 5) {
    user.confirm = true
    user.save()
  } else {
    return res.send('Срок действия ссылки истёк')
  }
  writeToFileLog(`
    Пользователь 
    id: ${user.id}
    Инициалы: ${user.surname} ${user.name} 
    запросил подтверждение регистрации в ${new Date()}
  `)
  return res.sendFile(__dirname + '/index.html')
})

app.post('/api/registration', async (req, res) => {
  writeToFileLog(`
    Пользователь 
    id: ${req.body.Login}
    Инициалы: ${req.body.surname} ${req.body.name} 
    запросил регистрацию на клиенте в ${new Date()}
  `)
  let listblockstr = await blockedDublicate(
    req.body.Login,
    req.body.Email, 
    req.body.Role
  )
  let user = await createUser(
    req.body
  )
  IsMail = await mailSend(
    user.id,
    uri,
    user.email,
    req.body.Role,
    listblockstr
  )
  if (
    listblockstr == errorDB ||
    user == errorDB
  ) {
    return res.end(errorDB)
  }
  if (IsMail) {
    if (listblockstr) {
      user.duplicate = true
      user.save()
      setTimeout(() => {
        user.duplicate = false
        user.save()
      }, 1000 * 60 * 68 * 24)
    }
    return res.end(
      JSON.stringify(user)
    )
  } else {
    await user.destroy();
    return res.end('Ошибка отправки сообщения на почту')
  }
})

app.get('/api/books/:id/:role', async (req, res) => {
  let queryUser;
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
    return res.end(
      JSON.stringify(newFormBooks)
    )
  }
  return res.end('not found')
})

app.get('/api/writters/list/:id/:role', async (req, res) => {
  let queryUser;
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
async function mailSend(id, uri, mail, role, duplicatestr) {
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
  let info = ''
  if (duplicatestr.indexOf('mail') > -1) {
    info += ' У вас повторяется email.'
  }
  if (duplicatestr.indexOf('login') > -1) {
    info += ' У вас повторяется логин.'
  }
  let infoblock = 'Учетная запись недоступна в течение 24 часов.'
  try {
    info = await transporter.sendMail({
    from: '"RWO " <yatest2021@mail.ru>', // sender address
    to: mail, // list of receivers
    subject: "Hello,confirm ur profile, pls! ✔", // Subject line
    // text: ``, // plain text body
    html: `<a href="${uri}/api/confirm/${role}/${id}">
    Кликните по тексту, чтобы подтвердить почту и открыть приложение.
    </a> ${info != '' ? info + infoblock: ''}`, // html body
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
  })
}

function createReportFile(listXML, user, nameList) {
  let builder = require('xmlbuilder');
  let doc = builder.create('root', { encoding: 'utf-8' })
  for (let item of listXML) {
    doc
      .ele(nameList)
        .ele('Наименование')
          .txt(item.name + ' ' + item.surname)
          .up()
        .ele('ДатаРегистрации')
          .txt('' + item.createdAt)
          .up()
        .ele('email')
          .txt(item.email)
          .up()
        .ele('login')
          .txt(item.login)
          .up()
        .ele('Подтверждён')
          .txt(item.confirm)
          .up()
  }
  let path = `${v4()}USER=${user.name}.xml`
  fs.appendFile(__dirname + `/Reports/${path}`, doc.end({ pretty: true }), err => {
    console.log('Xml add')
    if(err) throw err;
  })
  return path
}

async function blockedDublicate(login, email, role) {
  try {
    let duplicate = ''
    let DuplicateOfferEmail = null
    let DuplicateOfferLogin = null
    if (role == "Инвестор") {
      DuplicateOfferEmail = await Offers.findOne({
        where: {
          email: email
        }
      })
      DuplicateOfferLogin = await Offers.findOne({
        where: {
          login: login
        }
      })
    } else if (role == "Читатель") {
      DuplicateOfferEmail = await Readers.findOne({
        where: {
          email: email
        }
      })
      DuplicateOfferLogin = await Readers.findOne({
        where: {
          login: login
        }
      })
    } else if (role == "Писатель") {
      DuplicateOfferEmail = await Writters.findOne({
        where: {
          email: email
        }
      })
      DuplicateOfferLogin = await Writters.findOne({
        where: {
          login: login
        }
      })
    }
    if (DuplicateOfferEmail) {
      duplicate += ' mail'
      DuplicateOfferEmail.duplicate = true
      DuplicateOfferEmail.save()
    }
    if (DuplicateOfferLogin) {
      duplicate += ' login'
      DuplicateOfferLogin.duplicate = true
      DuplicateOfferLogin.save()
    }
    if (duplicate != '') {
      setTimeout(() => {
        user.confirm = false
        user.save()
      }, 1000 * 60 * 60 * 24)
    }
    return duplicate
  } catch (error) {
    return errorDB
  }
}

async function createUser(jsonUser) {
  try {
    let loremUser = {
      login: jsonUser.Login,
      password: jsonUser.Password,
      name: jsonUser.Name,
      surname: jsonUser.Surname,
      email: jsonUser.Email,
      secret_code: jsonUser.SecretCode,
    }
    if (jsonUser.Role == 'Читатель') {
      return await Readers.create(loremUser)
    }
    else if (jsonUser.Role == 'Писатель') {
      return await Writters.create(loremUser)
    }
    else if (jsonUser.Role == 'Инвестор') {
      return await Offers.create(loremUser)
    }
  } catch (error) {
    return errorDB
  }
}

async function validateUser(user) {
  try {
    let response
    if (user.Role == 'Читатель') {
      response = await Readers.findOne({
        where: {
          login: user.Login,
          password: user.Password
        },
        attributes: [
          'id', 'name',
          'surname', 'email',
          'date_of_born', 'confirm',
          'duplicate'
        ]
      })
    } else if (user.Role == 'Писатель') {
      response = await Writters.findOne({
        where: {
          login: user.Login,
          password: user.Password
        },
        attributes: [
          'id', 'name',
          'surname', 'email',
          'work_experience', 'confirm',
          'duplicate'
        ]
      })
    } else if (user.Role == 'Инвестор') {
      response = await Offers.findOne({
        where: {
          login: user.Login,
          password: user.Password
        },
        attributes: [
          'id', 'name',
          'surname', 'email',
          'note', 'confirm',
          'duplicate'
        ]
      })
    }
    return response
  } catch (error) {
    return errorDB
  }
}
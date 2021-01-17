const Sequelize = require('sequelize')

let readers = {
  id: {
    type: Sequelize.INTEGER,
    autoIncrement: true,
    primaryKey: true,
    allowNull: false
  },
  login: {
    type: Sequelize.STRING(16),
    allowNull: false
  },
  password: {
    type: Sequelize.STRING,
    allowNull: false
  },
  name: {
    type: Sequelize.STRING(32),
    allowNull: false
  },
  surname: {
    type: Sequelize.STRING(32),
    allowNull: false
  },
  email: {
    type: Sequelize.STRING(256),
    allowNull: false
  },
  secret_code: {
    type: Sequelize.STRING,
    defaultValue: false
  },
  date_of_born: {
    type: Sequelize.DATEONLY
  }
}

let age_limits = {
  id: {
    type: Sequelize.INTEGER,
    autoIncrement: true,
    primaryKey: true,
    allowNull: false
  },
  value: {
    type: Sequelize.INTEGER(2),
    allowNull: false
  }
}

let writters = {
  id: {
    type: Sequelize.INTEGER,
    autoIncrement: true,
    primaryKey: true,
    allowNull: false
  },
  login: {
    type: Sequelize.STRING(16),
    allowNull: false
  },
  password: {
    type: Sequelize.STRING,
    allowNull: false
  },
  secret_code: {
    type: Sequelize.STRING,
    allowNull: false
  },
  name: {
    type: Sequelize.STRING(32),
    allowNull: false
  },
  surname: {
    type: Sequelize.STRING(32),
    allowNull: false
  },
  email: {
    type: Sequelize.STRING(256),
    allowNull: false
  },
  work_experience: {
    type: Sequelize.INTEGER(2),
    allowNull: true
  },
}

let offers = {
  id: {
    type: Sequelize.INTEGER,
    autoIncrement: true,
    primaryKey: true,
    allowNull: false
  },
  login: {
    type: Sequelize.STRING(16),
    allowNull: false
  },
  password: {
    type: Sequelize.STRING,
    allowNull: false
  },
  secret_code: {
    type: Sequelize.STRING,
    allowNull: false
  },
  name: {
    type: Sequelize.STRING(32),
    allowNull: false
  },
  surname: {
    type: Sequelize.STRING(32),
    allowNull: false
  },
  email: {
    type: Sequelize.STRING(256),
    allowNull: false
  },
  note: {
    type: Sequelize.TEXT,
    allowNull: true
  },
}

let compositions = {
  id: {
    type: Sequelize.INTEGER,
    autoIncrement: true,
    primaryKey: true,
    allowNull: false
  },
  name_composition: {
    type: Sequelize.STRING(128),
    allowNull: false
  },
  genre: {
    type: Sequelize.STRING(24),
    allowNull: false
  },
  text_composition: {
    type: Sequelize.TEXT,
    allowNull: false
  }
}

let languages = {
  id: {
    type: Sequelize.INTEGER,
    autoIncrement: true,
    primaryKey: true,
    allowNull: false
  },
  name: {
    type: Sequelize.STRING(5),
    allowNull: false
  }
}

let sponsorship = {
  id: {
    type: Sequelize.INTEGER,
    autoIncrement: true,
    primaryKey: true,
    allowNull: false
  }
}

let rating = {
  id: {
    type: Sequelize.INTEGER,
    autoIncrement: true,
    primaryKey: true,
    allowNull: false
  }
}

module.exports = {
  languages,
  compositions, rating,
  age_limits, readers,
  offers, writters,
  sponsorship
}
const Sequelize = require('sequelize')
const DBCONFIG = require("./db.config");

function conSeq() {
  return new Sequelize(DBCONFIG.DB, DBCONFIG.USER, DBCONFIG.PASSWORD, {
    dialect: DBCONFIG.dialect,
    host: DBCONFIG.HOST,
    logging: false
  });
}

module.exports = conSeq
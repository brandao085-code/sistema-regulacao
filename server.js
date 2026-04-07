const express = require('express');
const mysql = require('mysql2');

const app = express();

const connection = mysql.createConnection(process.env.MYSQL_URL);

connection.connect((err) => {
  if (err) {
    console.error('Erro ao conectar:', err);
    return;
  }
  console.log('Conectado ao banco 🚀');
});

app.get('/pacientes', (req, res) => {
  connection.query('SELECT * FROM pacientes', (err, results) => {
    res.json(results);
  });
});

app.listen(3000, () => {
  console.log('Servidor rodando 🚀');
});
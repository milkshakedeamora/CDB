CREATE DATABASE db_projetoBanco
USE db_projetoBanco
CREATE TABLE tb_TipoConta (
    tipoContaId INT PRIMARY KEY,
    conta VARCHAR(20) NOT NULL
)

INSERT INTO tb_TipoConta 
VALUES (0, 'Corrente'), (1, 'Poupança')

CREATE TABLE tb_TipoCliente (
    tipoClienteId INT PRIMARY KEY,
    cliente VARCHAR(20) NOT NULL
)

INSERT INTO tb_TipoCliente
VALUES (0, 'Comum'), (1, 'Super'),(2,'Premium');

CREATE TABLE tb_conta (
  contaId INT PRIMARY KEY IDENTITY(1,1),
  numero VARCHAR(50) UNIQUE NOT NULL,
  saldo DECIMAL(10,2) NOT NULL,
  tipo INT,
  FOREIGN KEY (tipo) REFERENCES tb_TipoConta(tipoContaId),
)

CREATE TABLE tb_contaPoupanca (
  numero VARCHAR(50) PRIMARY KEY,
  taxaRendimento DECIMAL,
  FOREIGN KEY (numero) REFERENCES tb_conta(numero)
  
)

CREATE TABLE tb_contaCorrente (
  numero VARCHAR(50) PRIMARY KEY,
  taxaManutencao DECIMAL,
  FOREIGN KEY (numero) REFERENCES tb_conta(numero)
)



CREATE TABLE tb_cliente (
  clienteId INT PRIMARY KEY IDENTITY(1,1),
  nome VARCHAR(100) NOT NULL,
  cpf VARCHAR(11) NOT NULL UNIQUE,
  senha VARCHAR(20)NOT NULL,  
  dataNascimento DATETIME,
  tipo INT, 
  conta INT,
  FOREIGN KEY (tipo) REFERENCES tb_TipoCliente(tipoClienteId),
  FOREIGN KEY (conta) REFERENCES tb_conta(contaId) 
)






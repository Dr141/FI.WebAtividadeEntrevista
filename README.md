**TESTE PRÁTICO**

- Foi realizado atualização na base de dados:
  - ALTER TABLE CLIENTES ADD CPF VARCHAR(11) NOT NULL;
  - CREATE UNIQUE INDEX INDEX_CPF ON CLIENTES(CPF);
  - CREATE UNIQUE INDEX INDEX_CLIENTE ON BENEFICIARIOS(CPF, IDCLIENTE)
  - Implementado procedures para o beneficiario
  - Atualizado procedures do cliente
 
- Base de dados para teste:
  - [BancoDeDados.7z](https://drive.google.com/file/d/1rFuC6RT1VptEOhNy_dRk_XyzHqJZ0zcX/view?usp=sharing)    

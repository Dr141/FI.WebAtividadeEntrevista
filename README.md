**TESTE PRÁTICO**

- Foi realizado atualização na base de dados:
  - ALTER TABLE CLIENTES ADD CPF VARCHAR(11) NOT NULL;
  - CREATE UNIQUE INDEX INDEX_CPF ON CLIENTES(CPF);
  - CREATE UNIQUE INDEX INDEX_CLIENTE ON BENEFICIARIOS(CPF, IDCLIENTE) 

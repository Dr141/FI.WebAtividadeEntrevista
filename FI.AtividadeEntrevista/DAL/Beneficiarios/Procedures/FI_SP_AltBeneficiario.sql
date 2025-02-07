CREATE OR ALTER PROC FI_SP_AltBeneficiario
    @NOME          VARCHAR (50),
	@Id            BIGINT,
	@CPF		   VARCHAR (11),
	@IDCLIENTE     BIGINT
AS
BEGIN
	IF EXISTS(SELECT * FROM BENEFICIARIOS WHERE ID = @ID)
		UPDATE BENEFICIARIOS SET NOME = @NOME, CPF = @CPF
		WHERE Id = @Id
	ELSE
		EXECUTE FI_SP_IncBeneficiario @NOME, @CPF, @IDCLIENTE
END
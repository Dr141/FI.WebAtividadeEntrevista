$(document).ready(function () {
    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
        $('#formCadastro #CPF').val(obj.CPF);
        if (obj.beneficiarios) {
            $("table tbody").empty();
            obj.beneficiarios.forEach(beneficiario => {
                var newRow = `
                <tr>
                    <td>${beneficiario.CPF}</td>
                    <td>${beneficiario.Nome}</td>
                    <td>
                        <button class='btn btn-info btn-sm alterar'>Alterar</button>
                        <button class='btn btn-danger btn-sm remove'>Remover</button>
                    </td>
                </tr>`;
                $("table tbody").append(newRow);
            });
        }
    }

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        // Remover a máscara dos campos antes de enviar
        var cpf = $('#CPF').cleanVal()
        var telefone = $('#Telefone').cleanVal()
        var cep = $('#CEP').cleanVal()
        var beneficiarios = [];
        $("table tbody tr").each(function () {
            var cpf = $(this).find("td:eq(0)").text().replace(/[.\-]/g, "");
            var nome = $(this).find("td:eq(1)").text();
            beneficiarios.push({ CPF: cpf, Nome: nome });
        });

        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": cep,
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": telefone,
                "CPF": cpf,
                "beneficiarios": beneficiarios
            },
            error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success:
            function (r) {
                ModalDialog("Sucesso!", r)
                $("#formCadastro")[0].reset();
                $("table tbody").empty();
                window.location.href = urlRetorno;
            }
        });
    })

    $(".btn-success").click(function () {
        var cpf = $("#CPFBeneficiario").val()
        var nome = $("#NomeBeneficiario").val()
        if (cpf && nome) {
            var newRow =
                `<tr>
                    <td>${cpf}</td>
                    <td>${nome}</td>
                    <td>
                        <button class='btn btn-info btn-sm alterar'>Alterar</button>
                        <button class='btn btn-info btn-sm remove'>Remover</button>
                    </td>
                </tr>`
            $("table tbody").append(newRow);
            $("#formBeneficiario")[0].reset();
        } 
    })

    $("table").on("click", ".alterar", function () {
        var row = $(this).closest("tr");
        var cpf = row.find("td:eq(0)").text();
        var nome = row.find("td:eq(1)").text();

        $("#CPFBeneficiario").val(cpf);
        $("#NomeBeneficiario").val(nome);
        row.remove()
    })

    $("table").on("click", ".remove", function () {
        $(this).closest("tr").remove()
    })

    $("#CPF").mask("999.999.999-99")
    $("#CPFBeneficiario").mask("999.999.999-99")
    $("#CEP").mask("99999-999")
    $("#Telefone").mask("(99) 9 9999-9999")
    
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

function ModalBeneficiarios() {
    $('#myModal').modal('show')
}
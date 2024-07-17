$(document).ready(function () {
    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        // Remover a máscara dos campos antes de enviar
        var cpf = $('#CPF').cleanVal()
        var telefone = $('#Telefone').cleanVal()
        var cep = $('#CEP').cleanVal()
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
                "CPF": cpf
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
            }
        });
    })
    $("#CPF").mask("999.999.999-99")
    $("#CEP").mask("99999-999")
    $("#Telefone").mask("(99) 9 9999-9999")
})
//function FormatCpfFunction(element) {
//    if (element.value.length > 0) {     
//        element.value = element.value.replace(/\D/g, '')
//        switch (element.value.length) {
//            case 4:
//                element.value = `${element.value.substring(0, 3)}.${element.value.substring(3, 4)}`
//                break
//            case 6:
//                element.value = `${element.value.substring(0, 3)}.${element.value.substring(3, 6)}`
//                break
//            case 9:
//                element.value = `${element.value.substring(0, 3)}.${element.value.substring(3, 6)}.${element.value.substring(6, 9)}`
//                break
//            case 11:
//                element.value = `${element.value.substring(0, 3)}.${element.value.substring(3, 6)}.${element.value.substring(6, 9)}-${element.value.substring(9, 11)}`
//                break
//        }
//    }    
//}

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
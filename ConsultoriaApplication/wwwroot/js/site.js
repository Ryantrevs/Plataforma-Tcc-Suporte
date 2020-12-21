var activeList = "";

$("#id").keyup(function () {
    var url = "/Home/getId";
    var nome = $(this).val();
    console.log(nome);
    $.post(url, { Nome: nome }, function (data) {
        complete = data;
        console.log(data);
        $("#id").autocomplete({
            source: data
        });
    });

});


$("#AddScope").click(function () {
    var url = "/Academico/InsertScope";
    var TaskListId = activeList;
    $.post(url, { taskId: TaskListId }, function (data) {
        console.log(data)
        var coluna = "<div class=coluna id=" + data + ">"
        coluna += "<input class='titulo' type=text value='insira um titulo aqui'> </input></div>"
        var html = $.parseHTML(coluna);
        $(".Task").append(html);
        $(".titulo").focusout(function () {
            url = "/Academico/changeScopeTitule"
            var parent = this.closest('div')
            $.post(url, { titule: this.value, id: parent.id }, function (response) {
                //console.log(response)
            })
        })
    })
    

})

$("#AddLista").click(function () {
    var url = "/Academico/InsertTaskList";
    var titulo = "Nova Lista"
    $.post(url, { Titulo: titulo }, function (data) {
        var newList = '<a id="list' + data + '" class="List">';
        newList += '<div class="link">' + titulo + '<input type="hidden" value="' + data + '"/></div ><div class="config"><button class="ListConfig" type="button">teste<i class="fas fa-cog"></i></button></div ></a > '
        var html = $.parseHTML(newList);
        $(".navLateral").append(html);
        $(html).click(function () {
            var conf = this.closest(".List");
            var link = conf.children[0].textContent;
            var id = conf.id.toString().split('list')[1];
            $("#menuListId").val(id);
            $("#NameList").val(link)
            var url = "/Academico/UsersOnList";
            var dados = post(url, { Id: id });
            dados.then(t => {
                $.each(t, function (key, value) {
                    var option = '<option value=' + value.id + '>' + value.nome + '</option>'
                    var html = $.parseHTML(option)
                    $("#usersOnList").append(html);
                })
                MoveDiv(".menuListConfig", "0em", "left");

            });
        });

    });
    
})

function GetCard(id) {
    var url = "/Academico/GetCard";
    $.post(url, { Id: id }, function (data) {
        console.log(data);
        buildMenu(data);
        return;
    });
}


$(".background").click(function () {
    MoveMenu("-200em")
    updateCard();
})

function MoveMenu(distance) {
    $('.menu').animate({
        right: distance
    }, 200);
}

$(document).ready(function () {
    var elems = $(".navLateral").children();
    GetTasks(elems[0].id.toString().split("list")[1]);
    //var valor = $("#list").children();
    //GetTasks(valor[0].children[0].value);
})

function GetTasks(id) {
    var url = "/Academico/GetTasks";
    //console.log(id);
    $.post(url, { Id: id }, function (data) {
        $(".Task").empty();
        BuildTasks(data)
        activeList = id;

    }); 
}

$("#submitCard").click(function () {
    var id = $('#listTask').val();
    var titule = $('.tituleMenuTask').val();
    var describe = $('.describe').val();
    var url = "/Academico/InsertCard";
    $.post(url, { ScopeId: id, Titule: titule, describe: describe }, function (data) {
        console.log(titule);
        var div = "<div class=lista id=" + data + ">";
        var card = '<div class=card></h5>';
        var h5 = "<h5>" + titule + "</h5>";
        input = "<input type=hidden value=" + data + " />"
        card += h5 + input + '</div>';
        div += card + '<div class=excluir></div>';
        div += "</div>"
        var html2 = $.parseHTML(div);
        $("#" + id).append(html2);
        $(document).ready(function () {
            $('.lista').draggable({ helper: 'clone' });
            $('.coluna').droppable({
                accept: '.lista',
                drop: function (ev, ui) {
                    var dropItem = $(ui.draggable)
                    $(this).append(dropItem);

                    var url = "/Academico/ChangeCardScope";
                    $.post(url, { IdCard: dropItem[0].id, ScopeId: this.id }, function (data) {
                        console.log(data);
                        return;
                    });
                    console.log("meu escopo ", this.id);
                    console.log("alvo", dropItem[0].id);
                }
            });
        });
        $("#" + data).click(function () {
            GetCard(this.id)
            MoveMenu("0em")
        });

    });
});

$("#closeMenu").click(function () {
    $('.menuTask').animate({
        left: "-200em"
    }, 200);
    updateCard();
})

$("#AddTarefa").click(function () {
    var scopes = $(".coluna");
    $.each(scopes, function (key, value) {
        var option = '<option value="' + value.id + '">' + value.children[0].value + '</option>'
        var html = $.parseHTML(option)
        $("#listTask").append(html)
    })
    $('.menuTask').animate({
        left: "0em"
    }, 200);
});

$(".ListConfig").click(function () {
    var conf = this.closest(".List");
    var link = conf.children[0].textContent;
    var id = conf.id.toString().split('list')[1];
    $("#menuListId").val(id);
    $("#NameList").val(link)
    var url = "/Academico/UsersOnList";
    var dados = post(url, { Id: id });
    dados.then(t => {
        $.each(t, function (key, value) {
            var option = '<option value=' + value.id + '>' + value.nome + '</option>'
            var html = $.parseHTML(option)
            $("#usersOnList").append(html);
        })
        MoveDiv(".menuListConfig", "0em", "left");

    });
});

$("#closeMenu").click(function () {
    $('.menuTask').animate({
        left: "-200em"
    }, 200);
})

$("#AddTarefa").click(function () {
    var scopes = $(".coluna");
    $.each(scopes, function (key, value) {
        var option = '<option value="' + value.id + '">' + value.children[0].value + '</option>'
        var html = $.parseHTML(option)
        $("#listTask").append(html)
    })
    $('.menuTask').animate({
        left: "0em"
    }, 200);
});

$(".ListConfig").click(function (e) {
    var conf = this.closest(".List");
    var link = conf.children[0].textContent;
    $("#menuListId").val(conf.id.toString().split('list')[1]);
    $("#NameList").val(link)
    $(".menuListConfig").animate({
        left: "0em"
    }, 200)
})



function BuildTasks(data) {
    $.each(data, function (key, value) {
        //alert(key + ": " + value);
        var coluna = "<div class=coluna id=" + value.id + '>'
        coluna += "<input class=titulo type=text value=" + '"' + value.titulo + '"' + "><i class='fas fa-trash-alt' id='excludeScope'></i></div>";
        var html = $.parseHTML(coluna);
        
        $(".Task").append(html);
        $.each(value.cards, function (chave, valor) {
            var div = "<div class=lista id=" + valor.id + ">";
            var card = "<div class=card>";
            var h5 = "<h5>" + valor.titulo + "</h5>"
            input = "<input type=hidden value=" + valor.id + " />"
            card += h5 + input + "</div>";
            div += card + '<div class=excluir></i></div>';
            div += "</div>"
            var html2 = $.parseHTML(div);
            $("#" + value.id).append(html2);
            $("#" + valor.id).click(function () {
                var child = $(this).children()
                var childCard = child[0].children
                GetCard(childCard[1].value)
                MoveMenu("0em")
            })
            
        })

    });
    $("#excludeScope").click(function () {
        removeScope($(this).parent()[0].id);
    })
    $(".titulo").focusout(function () {
        url = "/Academico/changeScopeTitule"
        var parent = this.closest('div')
        $.post(url, { titule: this.value, id: parent.id }, function (response) {
            console.log(response)
        })
    })
    $(document).ready(function () {
        $('.lista').draggable({ helper: 'clone' });
        $('.coluna').droppable({
            accept: '.lista',
            drop: function (ev, ui) {
                var dropItem = $(ui.draggable)
                $(this).append(dropItem);

                var url = "/Academico/ChangeCardScope";
                $.post(url, { IdCard: dropItem[0].id, ScopeId: this.id }, function (data) {
                    console.log(data);
                    return;
                });
                console.log("meu escopo ", this.id);
                console.log("alvo", dropItem[0].id);
            }
        });
    });
}

$(".icon-exclude").click(function () {
    var parent = this.closest('.lista')
    var url = "/Academico/DeleteCard"
    $.post(url, { Id: parent.id }, function (data) {
        console.log(data);
        $(parent).remove();
    });
})


$(".icon-exclude").click(function () {
    var parent = this.closest('.lista')
    var url = "/Academico/DeleteCard"
    $.post(url, { Id: parent.id }, function (data) {
        console.log(data);
        $(parent).remove();
    });
})


$("#check").click(function () {
    if ($(".menuLateral").css("width") == "310px") {
        $(".menuLateral").css("width", "60px");
        $(".Task").css("width", "90%");
        $(".btnTarefas").css("width", "60px");    //Todos os botões foram trocas para o seu id, menos o 
        $("#AddTarefa").html("+T");
        $("#AddLista").html("+L");
        $("#AddScope").html("+E");
        $(".link").css("font-size", "0pt")
    }
    else {
        $(".menuLateral").css("width", "310px");
        $(".Task").css("width", "70%");
        $(".btnTarefas").css("width", "5em");
        $("#AddTarefa").html("+Tarefa");
        $("#AddLista").html("+Lista");
        $("#AddScope").html("+Escopo");
        $(".link").css("font-size", "12pt")
    }
});


    function BuildCard(obj) {
        $.each(obj, function (chave, valor) {
            var div = "<div class=lista>";
            var card = "<div class=card>";
            var h5 = "<h5>" + valor.titulo + "</h5>"
            input = "<input type=hidden value=" + valor.id + " />"
            card += h5 + input + "</div>";
            div += card + "</div>"
            var html2 = $.parseHTML(div);
            $(".coluna").append(html2);
            $(".lista").click(function () {
                var child = $(this).children()
                var childCard = child[0].children
                GetCard(childCard[1].value)
                MoveMenu("0em")
            })
        })
    }

    $(".icon-close").click(function () {
        MoveMenu('-200em');
    })

$('.Task').click(function () {
    if ($('.menu').css('left') == '0px') {
        MoveMenu('-20em');
    }
})

async function updateCard() {
    var elementos = $(".CardBackgroud").children();
    var titule = $(elementos[1]).children()[2].value;
    var describe = $(elementos[1]).children()[6].value;
    var porc = $(elementos[1]).children()[10].value;
    var id = $(elementos[1]).children()[11].value;
    var url = "/Academico/ChangeCard";
    $.post(url, { Id: id, Titule: titule, Describe: describe, Porc: porc }, function (data) {
        console.log(data);
        GetTasks(activeList)
    })
    console.log(activeList);
};

function buildMenu(data) {
    var card = $(".CardBackgroud").children();
    var inputs = $(card[1]).children()
    console.log(inputs)
    $(inputs[2]).val(data.titulo);
    $(inputs[6]).val(data.descricao);
    $(inputs[10]).val(data.porcentagem).change();
    $(inputs[11]).val(data.id);
    
}
$("#back").click(function () {
    $('.menuTask').animate({
        left: "-200em"
    }, 200);
});

$("#addUser").click(function () {
    var convite = '<label>Digite o e-mail de quem deseja adicionar</label><input type="email" id="UserEmail"/><button type="button" id="SendInvite">Enviar</button>'
    var html = $.parseHTML(convite);
    $(".AddPersontoList").append(html);
    $('document').ready(function () {
        $('#SendInvite').attr('disabled', 'disabled');
        $("#UserEmail").keyup(function () {
            isEmail($("#UserEmail").val());
        });
        $("#SendInvite").click(function () {
            sendInvite($("#UserEmail").val());
            
        });
    });
   
});

function isEmail(email) {
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (filter.test(email)) {
        $('button').removeAttr('disabled');
    } else {
        $('#SendInvite').attr('disabled', 'disabled');
    }
}

function sendInvite(email) {
    var url = "/Academico/AddUserOnList";
    var id = $("#menuListId").val()
    var obj = { Email: email, Id: id };
    var response = post(url, obj);
    response.then(t => {
        console.log(t);
        //COLOCAR ADIÇÃO DE <OPTION> QUANDO IMPLEMENTAR CONTROLE DE USUÁRIO
    })
    
}
$("#ExcludeUser").click(function () {
    var usuario = $("#usersOnList").val();
    var id = $("#menuListId").val();
    var url = "/Academico/ExcludeUserOnList";
    var resp = post(url, { UserId: usuario, Id: id });
    resp.then(t => {
        console.log({ UserId: usuario, Id: id })
        console.log(t)
    })
})

async function post(url, obj) {
    $.ajaxSetup({ async: false });
    var obj
    $.post(url, obj, function (dados) {
        obj = dados;
    });
    $.ajaxSetup({ async: true });
    return obj;
}

async function MoveDiv(element, distance, direction) {
    if (direction == "left") {
        $(element).animate({
            left: distance
        }, 200)
        return
    } else {
        $(element).animate({
            right: distance
        }, 200)
        return
    }
}

$("#excludeList").click(function () {
    var id = $("#menuListId").val();
    var url = "/Academico/ExcludeList";
    var obj = { Id: id }
    var response = post(url, obj);
    response.then(t => {
        MoveDiv(".menuListConfig", "-200em", "left");
        $("#list" + id).remove();
    });
});

async function updateTitule() {
    var id = $("#menuListId").val();
    var titule = $("#NameList").val();
    var url = "/Academico/UpdateTitule";
    var response = post(url, { Id: id, Titule: titule })
    response.then(t => {
        $("#list" + id).children()[0].textContent = titule
    });
}

$("#backgroundConfig").click(function () {
    MoveDiv(".menuListConfig", "-200em", "left");
    updateTitule();
});
async function removeScope(id) {
    var url = "/Academico/RemoveScope"
    var obj = { Id: id }
    var response = post(url, obj)
    response.then(t => {
        $("#" + id).remove();
    })
}

//pagina de cadastro de venda

//mascara de form do telefone

function mascaraNumero(id) {
    console.log("alterou");
    var numero = $("#" + id);
    console.log(typeof numero.val());
    if (numero.val() == 0)
        numero.val('(' + telefone.value) ; //quando começamos a digitar, o script irá inserir um parênteses no começo do campo.
    if (numero.val() == 3)
        numero.val(telefone.value + ') '); //quando o campo já tiver 3 caracteres (um parênteses e 2 números) o script irá inserir mais um parênteses, fechando assim o código de área.
}
//(21)9 9999 9999

//======== validação de inputs


$("#formCadVenda div").hide();
$("#nomeDiv").show();

async function verificaCliente(id) {//adiciona inputs de cliente automatiamente
    console.log("entrou");
    console.log(id);
    $("#formCadVenda div").show();
    var valEmail = $("#" + id).val();
    console.log(valEmail)
    var nomeCli = $("#nomeCliente");
    var sexoCli = $("#sexo");
    var telCli = $("#telefoneCliente");
    
    //limpa inputs
    nomeCli.val(null);
    sexoCli.val("").change();
    telCli.val(null);
    $("#avisoCliente").text('');
    //traz informação do banco
    $.get("/Sale/GetDadosCliente", { mail: valEmail }, function (data) {
        console.log(data);
        if (data == null) {
            //cliente nao encontrado
            console.log("Não encontrou cliente");
        } else {//encontrou
            console.log("Encontrou cliente");
            nomeCli.val(data.name);//passa valor pro input
            if (data.sex == "masculino") {
                sexoCli.val("masculino").change();
            } else if (data.sex == "feminino") {
                sexoCli.val("feminino").change();
            }
            telCli.val(data.tel);
            //nomeCli.attr('type', 'hidden');//muda pra hiddden
            //telCli.attr('type', 'hidden');
            $(".infoCli").hide();//esconde div
            $("#avisoCliente").text("Cliente encontrado na base, informações preenchidas automaticamente!");
        }
    });
}

//======================cadastra venda
$(document).ready(function () {//impede de dar submit no click
    $("#submitCadVenda").click(function (event) {
        event.preventDefault();
    });
});

function validaForm() {
    var form = $("#formCadVenda");
    var valid = true;
    var erros = "Error: ";
    var valPago = $("#valPago").val();
    var valTotal = $("#valTotal").val();
    var numPag = $("#numPag").val();
    var prev1 = new Date($("#prev1").val());
    var prev2 = new Date($("#prev2").val());
    var prev3 = new Date($("#prev3").val());
    var dtEntrega = new Date($("#dtEntrega").val());
    var telCli = $("#telefoneCliente").val();

    if (telCli.length !== 11) {
        erros = erros + "Telefone precisa conter 11 digitos!\n\n";
        valid = false;
        console.log(erros);
    }

    if (prev1.getTime() > prev2.getTime()) {
        erros = erros + "Data da previa 2 anterior a previa 1!\n\n";
        valid = false;
        console.log(erros);
    } else if (prev2.getTime() > prev3.getTime()) {
        erros = erros + "Data da previa 3 anterior a previa 2!\n\n";
        valid = false;
        console.log(erros);
    }


    if (prev3.getTime() > dtEntrega.getTime()) {
        erros = erros + "Data de entrega precisa ser posterior as previas!\n\n";
        valid = false;

    }
    if (valTotal < valPago) {
        erros = erros + "Valor Pago invalido,valor menor que valor total! \n\n";
        valid = false;

    }
    if (numPag < 1) {
        erros = erros + "Numero de paginas inválida!\n\n";
        valid = false;

    }
    console.log(erros);
    //alert(valid +"\n" + valPago + "\n" + valTotal + "\n" + prev1 + "\n" + prev2 + "\n" + prev3 + "\n" + dtEntrega);
    if (valid === true) {
        form.submit();//aplica submit
    } else {
        alert(erros);
        form.preventDefault();
    }
}


//=======================lista sales===============================
function abreModal(id) {
    console.log("entrou");
    console.log(id)
    modal = $("#"+id);
    modal.css("display", "block");
}

function fechaModal(id) {
    modal = $("#"+id);
    modal.css("display", "none");
}
function fechaModal2(id, e,div) {
    if (e.target === div) {
        concole.log("div mae");
        modal = $("#" + id);
        modal.css("display", "none");
    } else {
        return
    }    
}
$("#myModal").click(function () {
    modal = $(".myModal");
    modal.css("display", "none");
    console.log("fecha");
});


function deletaSale(id) {
    console.log("entra");
    console.log(id);
    
    var url = "/Sale/DeleteSale";
    console.log(id);
    $.post(url, { Id: id }, function () {
        alert("Venda deletada:"+id);
        location.reload();
    });
    
}

//===========================editSale

$(document).ready(function () {//impede de dar submit no click
    $("#editSaleSubmit").click(function (event) {
        event.preventDefault();
    });
});
function validaFormEditSale() {
    var form = $("#editSaleForm");
    var valid = true;
    var erros = "Erros:";
    var numPag = $("#numPag").val();
    var prev1 = new Date($("#prev1").val());
    var prev2 = new Date($("#prev2").val());
    var prev3 = new Date($("#prev3").val());
    var dtEntrega = new Date($("#dtEntrega").val());
    var valTotal = $("#valTotal").val();
    var valPago = $("#valPago").val();
    if (prev1.getTime() > prev2.getTime()) {
        erros = erros + "Data da previa 2 anterior a previa 1!\n\n";
        valid = false;        
    } else if (prev2.getTime() > prev3.getTime()) {
        erros = erros + "Data da previa 3 anterior a previa 2!\n\n";
        valid = false;        
    }


    if (prev3.getTime() > dtEntrega.getTime()) {
        erros = erros + "Data de entrega precisa ser posterior as previas!\n\n";
        valid = false;
    }
    if (valTotal < valPago) {
        erros = erros + "Valor Pago invalido,valor menor que valor total! \n\n";
        valid = false;
    }
    if (numPag < 1) {
        erros = erros + "Numero de paginas inválida!\n\n";
        valid = false;
    }

    if (valid === true) {        
        form.submit();
    } else {
        alert(erros);
    }
    
}

//----------------------------editClient


$(document).ready(function () {//impede de dar submit no click
    $("#editClientSubmit").click(function (event) {
        event.preventDefault();
    });
});

function validaFormEditClient() {
    var form = $("#editClientForm");
    var valid = true;
    var erros = "Erros: ";
    var telCli = $("#telefoneCliente").val();
    if (telCli.length !== 11) {
        erros = erros + "Telefone precisa conter 11 digitos!\n\n";
        valid = false;        
    }

    if (valid === true) {
        form.submit();
    } else {
        alert(erros);
    }
}
    $('.Task').click(function () {
        if ($('.menu').css('left') == '0px') {
            MoveMenu('-20em');
        }
    })

    $('#changeCard').click(function () {
        var elementos = $(".CardBackgroud").children();
        var titule = $(elementos[1]).val()
        var describe = $(elementos[4]).val();
        var porc = $(elementos[7]).val();
        var id = $(elementos[8]).val()
        var url = "/Academico/ChangeCard";
        $.post(url, { Id: id, Titule: titule, Describe: describe, Porc: porc }, function (data) {
            console.log(data);
        })
        console.log(activeList);
    });

    function buildMenu(data) {
        var card = $(".CardBackgroud").children();
        var inputs = $(card[1]).children()
        console.log(inputs)
        $(inputs[2]).val(data.titulo);
        $(inputs[6]).val(data.descricao);
        $(inputs[10]).val(data.porcentagem).change();
        $(inputs[11]).val(data.id);

}

    $("#back").click(function () {
        $('.menuTask').animate({
            left: "-200em"
        }, 200);
});


$(".addDescricao").click(function () {
    $(".addDescricao").hide("fast", function () {
        $(".conteudoDescricao").show();
    });
});
$(".mostrar").click(function () {
    var valor = $("textarea").val();
    $(".testeBoy").text(valor)
    $(".addDescricao").show();
    $(".conteudoDescricao").hide();
    console.log(valor)
});


$('#text').on('keyup onpaste', function () {
    var alturaScroll = this.scrollHeight;
    var alturaCaixa = $(this).height();

    if (alturaScroll > (alturaCaixa + 10)) {
        if (alturaScroll > 500) return;
        $(this).css('height', alturaScroll);
    }
});


class Carrinho {

    clickIncremento(button) {
        let data = this.getData(button);
        data.Quantidade++;
        this.postQuantidade(data);

    }
    clickDecremento(button) {
        let data = this.getData(button);
        data.Quantidade--;
        this.postQuantidade(data);
    }

    updateQuantidade(input) {
        let data = this.getData(input);
        this.postQuantidade(data);
    }
   

    getData(elemento) {
        var linhaDoItem = $(elemento).parents('[item-id]');
        var itemId = $(linhaDoItem).attr('item-id');
        var novaQuantidade = $(linhaDoItem).find('input').val();


        //Requisição AJAX
        //objeto que vai levar os dados para o cliente servidor 
       return {
            Id: itemId,
            Quantidade: novaQuantidade
        };
    }

    postQuantidade(data) {
        //Token anit-falsificação
        let token = $('[name = __RequestVerificationToken]').val();

        let headers = {};
        headers['RequestverificationToken'] = token;



        //padrão da requisição AJAX
        $.ajax({
            url: '/pedido/updatequantidade',
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            //faz parte do token antifalsificação
            headers: headers
        }).done(function (response) {
            //código de atualização da quantidade
            //val modifica o valor
            //find seleciona o elemento do html correspondente
            let itemPedido = response.itemPedido;
            let linhaDoItem = $('[item-id=' + itemPedido.id + ']')
            linhaDoItem.find('input').val(itemPedido.quantidade);
            linhaDoItem.find('[subtotal]').html((itemPedido.subtotal).duasCasas());
            let carrinhoViewModel = response.carrinhoViewModel;
            $('[numero-itens]').html('Total: ' + carrinhoViewModel.totalItens + ' itens');
        
            $('[total]').html((carrinhoViewModel.total).duasCasas());

          
           
            
            if (itemPedido.quantidade == 0) {
                linhaDoItem.remove();
            }

        })
    }
}

var carrinho = new Carrinho();


Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace(".", ",");
}
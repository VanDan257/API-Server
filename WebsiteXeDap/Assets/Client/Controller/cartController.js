var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#UpdateCart').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ProID: $(item).data('id')
                    }
                });
            })
            $.ajax({
                url: '/Home/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "../Home/viewCart"
                    }
                }
            })
        });
        $('#DeleteCart').off('click').on('click', function () {
            $.ajax({
                url: '/Home/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "../Home/viewCart"
                    }
                }
            })
        });
        $('.delete-product').off('click').on('click', function (e) {
            e.preventDefault;
            $.ajax({
                url: '/Home/Delete',
                data: {id: $(this).data('id')},
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "../Home/viewCart"
                    }
                }
            })
        });
    }
}

cart.init();
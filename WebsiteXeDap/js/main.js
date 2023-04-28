(function ($) {
    "use strict";

    let dataCartStorage = JSON.parse(localStorage.getItem('cart')) || [];
    let sumCart = document.querySelector('.sum-cart');
    sumCart.innerText = dataCartStorage.length || 0;
    

    function getParent(Element, Selector) {
        while (Element.parentElement) {
            if(Element.parentElement.matches(Selector)) {
                return Element.parentElement;
            } else {
                Element = Element.parentElement;
            }
        }
    }
    
    // get cart data
    function getDataCart() {
        if(document.getElementById('table-cart')) {
            const tableCart = document.querySelector('#table-cart tbody');
            const totalCart = document.querySelector('#total-cart');
            let result = 0;
            let total = 0;
            let htmls;

            if(dataCartStorage.length > 0) {
                htmls = dataCartStorage.map((item, index) => {

                    const formatter = new Intl.NumberFormat('vi-VN', {
                        style: 'currency',
                        currency: 'VND',
                        minimumFractionDigits: 0,
                      });
                    result += item.quantity * item.price;
                    total = formatter.format(result);

                    return `
                        <tr>
                            <td class="align-middle">${index + 1}</td>
                            <td class="align-middle"><img src="${item.imagePath}" alt="" style="width: 50px;"> ${item.title}</td>
                            <td class="align-middle">${formatter.format(item.price)}</td>
                            <td class="align-middle">
                                <div class="input-group quantity mx-auto" style="width: 100px;">
                                    <div class="input-group-btn">
                                        <button class="btn btn-sm btn-primary btn-minus" >
                                        <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                    <input type="text" class="form-control form-control-sm bg-gray-200 text-center" value="${item.quantity}">
                                    <input type="hidden" class="form-control" name="quantityUpdate" value="${item.quantity}">
                                    <div class="input-group-btn">
                                        <button class="btn btn-sm btn-primary btn-plus">
                                            <i class="fa fa-plus"></i>
                                        </button>
                                    </div>
                                </div>
                            </td>
                            <td class="align-middle">${formatter.format(item.price * item.quantity)}</td>
                            <td class="align-middle"><button class="btn-delete-cart btn btn-sm btn-primary" data-delete="${item.id}"> <i class="fa fa-times"></i></button></td>
                        </tr>
                    `;
                });

                // subTotalCart.innerText = result;
                totalCart.innerText = total;
                // totalCart.text(result + '₫');
            
                tableCart.innerHTML = htmls.join('');

            };


        }
    }

    function updateCart() {
        if(document.querySelector('.btn-update-cart') && dataCartStorage.length > 0) {
            const btnUpdateCart = document.querySelector('.btn-update-cart');
            let toastMessage = document.querySelector('.toast-message');
            const quantityUpdate = document.querySelectorAll('input[name="quantityUpdate"]');

            btnUpdateCart.onclick = () => {

                quantityUpdate.forEach((item, index) => {
                    if(item.value != dataCartStorage[index].quantity) {
                        dataCartStorage[index].quantity = item.value;
                    }
                });

                localStorage.setItem('cart', JSON.stringify(dataCartStorage));

                location.reload();


                // toastMessage.style.display = 'block';                        

                // setTimeout(() => {
                //     toastMessage.style.display = 'none';
                // }, 500)
            }
        }
    }

    function deleteCart() {
        if(document.querySelectorAll('.btn-delete-cart') && dataCartStorage.length > 0) {
            const btnDeleteCart = document.querySelectorAll('.btn-delete-cart');

            btnDeleteCart.forEach((item, index) => {
                item.onclick = () => {
                    getParent(item, 'tr').remove();

                    dataCartStorage.splice(index, 1);
    
                    localStorage.setItem('cart', JSON.stringify(dataCartStorage));

                    location.reload();

                    // getDataCart();
    
                    // toastMessage.style.display = 'block';                        
    
                    // setTimeout(() => {
                    //     toastMessage.style.display = 'none';
                    // }, 500)
                }
            })
        }
    }


    function handleCheckout() {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
            minimumFractionDigits: 0,
          });
        if(document.querySelector('.btn-proceed-ckeckout')) {
            const btnRedirect = document.querySelector('.btn-proceed-ckeckout');
            let toastMessage = document.querySelector('.toast-message');

            btnRedirect.onclick = (event) => {
                if(dataCartStorage.length <= 0) {
                    event.preventDefault();

                    toastMessage.style.display = 'block';                        
    
                    setTimeout(() => {
                        toastMessage.style.display = 'none';
                    }, 1000)
                }
            }

        }

        // render view checkout 
        if(dataCartStorage.length > 0 && document.querySelector('.order-products')) {
            const girdOrderProduct = document.querySelector('.order-products');
            let subTotalCheckout = document.querySelector('.subtotal-checkout');
            let totalCheckout = document.querySelector('.total-checkout');
            let result = 0;
            let total = 0;

            const htmls = dataCartStorage.map((item) => {
                result += item.price * item.quantity;
                total = formatter.format(result);
                return `
                    <div class="d-flex justify-content-between">
                        <h6>${item.title} x${item.quantity}</h6>
                        <p>${formatter.format(item.price * item.quantity)}</p>
                    </div>
                `;
            });

            girdOrderProduct.innerHTML = htmls.join('');
            totalCheckout.innerText = `${total}`;

        }
    }
    
    
    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }

        if($(this).scrollTop() > 240) {
            $('.header-nav').addClass('header-nav-fixed');
        } else {
            $('.header-nav').removeClass('header-nav-fixed');
        }

    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });


    // Vendor carousel
    $('.vendor-carousel').owlCarousel({
        loop: true,
        margin: 29,
        nav: false,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0:{
                items:2
            },
            576:{
                items:3
            },
            768:{
                items:4
            },
            992:{
                items:5
            },
            1200:{
                items:6
            }
        }
    });


    // Related carousel
    $('.related-carousel').owlCarousel({
        loop: true,
        margin: 29,
        nav: false,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0:{
                items:1
            },
            576:{
                items:2
            },
            768:{
                items:3
            },
            992:{
                items:4
            }
        }
    });

    // addDataCart();
    getDataCart();
    updateCart();
    deleteCart();
    handleCheckout();


    // Product Quantity
    $('.quantity button').on('click', function () {
        var button = $(this);
        var oldValue = button.parent().parent().find('input').val();
        if (button.hasClass('btn-plus')) {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            if (oldValue > 0) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 0;
            }
        }
        button.parent().parent().find('input').val(newVal);
    });

    // Silder main
    $(".slider-inner").slick({
        infinite: true,
        autoplay: true,
        autoplaySpeed: 3000,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1,
        cssEase: "ease-in-out",
        prevArrow: '<a type="button" class="slick-prev"><i class="fa-solid fa-chevron-left"></i></a>',
        nextArrow: '<a type="button" class="slick-next"><i class="fa-solid fa-chevron-right"></i></a>'
    })


    //Product detail slider
    $(".product-slick-inner").slick({
        infinite: true,
        autoplay: true,
        autoplaySpeed: 3000,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1,
        easing: "linner",
        prevArrow: '<a type="button" class="slick-product-prev"><i class="fa-solid fa-chevron-left"></i></a>',
        nextArrow: '<a type="button" class="slick-product-next"><i class="fa-solid fa-chevron-right"></i></a>',
        asNavFor: "#slick-slide-navfor"
    });

    $("#slick-slide-navfor").slick({
        speed: 500,
        slidesToShow: 3,
        slidesToScroll: 1,
        arrow: false,
        focusOnSelect: true,
        asNavFor: ".product-slick-inner"
    })

    
    // Customize 
    $('.hero__categories').on('mouseenter', function(){
        $('.hero__categories ul').slideToggle(400);
    });
    $('.hero__categories').on('mouseleave', function(){
        $('.hero__categories ul').hide(400);
    });

    $(window).on('load', function () {
        $(".loader").fadeOut();
        $("#preloder").delay(200).fadeOut("slow");

        /*------------------
            Gallery filter
        --------------------*/
        $('.featured__controls li').on('click', function () {
            $('.featured__controls li').removeClass('active');
            $(this).addClass('active');
        });
        if ($('.featured__filter').length > 0) {
            var containerEl = document.querySelector('.featured__filter');
            var mixer = mixitup(containerEl);
        }
    });
    // End customize
    
})(jQuery);



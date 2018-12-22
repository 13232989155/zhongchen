$(document).ready(function () {
    $('.slider nav .nav').mouseover(function () {
        $('.slider nav .nav').removeClass('nav-active');
        $(this).addClass('nav-active');
    });

    $('.slider nav .nav:nth-child(1)').mouseover(function () {
        $('.slider ul li').removeClass('active');
        $('.slider ul li:nth-child(1)').addClass('active');
    });
    $('.slider nav .nav:nth-child(2)').mouseover(function () {
        $('.slider ul li').removeClass('active');
        $('.slider ul li:nth-child(2)').addClass('active');
    });
    $('.slider nav .nav:nth-child(3)').mouseover(function () {
        $('.slider ul li').removeClass('active');
        $('.slider ul li:nth-child(3)').addClass('active');
    });
    $('.slider nav .nav:nth-child(4)').mouseover(function () {
        $('.slider ul li').removeClass('active');
        $('.slider ul li:nth-child(4)').addClass('active');
    });

    $('.menu-mob-a').click(function () {
        $('ul.menu').slideToggle();
    });

    setInterval(function () {

        if ($('.slider ul li.active').next('li').length) {
            $('.slider ul li.active').removeClass('active').next('li').addClass('active');
        } else {
            $('.slider ul li.active').removeClass('active');
            $('.slider ul li:first-of-type').addClass('active');
        }

        if ($('.slider nav .nav-active').next('.nav').length) {
            $('.slider nav .nav-active').removeClass('nav-active').next('.nav').addClass('nav-active');
        } else {
            $('.slider nav .nav-active').removeClass('nav-active');
            $('.slider nav .nav:first-of-type').addClass('nav-active');
        }

    }, 5000);

    $('.search-icon').click(function () {
        $('.search-popup').fadeIn();
    });
    $('.close-pop').click(function () {
        $('.search-popup').fadeOut();
    });

    $('html').hide(1);
    $(document).ready(function () {
        $('html').fadeIn(800);
    });

    // Select all links with hashes
    $('a[href*="#"]')
        // Remove links that don't actually link to anything
        .not('[href="#"]')
        .not('[href="#0"]')
        .click(function (event) {
            // On-page links
            if (
                location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '')
                &&
                location.hostname == this.hostname
            ) {
                // Figure out element to scroll to
                var target = $(this.hash);
                target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                // Does a scroll target exist?
                if (target.length) {
                    // Only prevent default if animation is actually gonna happen
                    event.preventDefault();
                    $('html, body').animate({
                        scrollTop: target.offset().top
                    }, 1000, function () {
                        // Callback after animation
                        // Must change focus!
                        var $target = $(target);
                        $target.focus();
                        if ($target.is(":focus")) { // Checking if the target was focused
                            return false;
                        } else {
                            $target.attr('tabindex', '-1'); // Adding tabindex for elements not focusable
                            $target.focus(); // Set focus again
                        };
                    });
                }
            }
        });

    /* Every time the window is scrolled ... */
    $(window).scroll(function () {
        /* Check the location of each desired element */
        $('.hideme').each(function (i) {

            var bottom_of_object = $(this).offset().top + $(this).outerHeight();
            var bottom_of_window = $(window).scrollTop() + $(window).height();

            /* If the object is completely visible in the window, fade it it */
            if (bottom_of_window > bottom_of_object) {
                $(this).animate({ 'opacity': '1' }, 1000);
            }
        });
    });

});
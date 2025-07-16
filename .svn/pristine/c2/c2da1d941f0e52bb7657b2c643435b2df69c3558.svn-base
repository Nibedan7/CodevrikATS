
Bar = (function () {
    function Bar() {
        this.progress = 0;
    }

    Bar.prototype.getElement = function () {
        var targetElement;
        if (this.el == null) {
            targetElement = document.querySelector(options.target);
            if (!targetElement) {
                throw new NoTargetError;
            }
            this.el = document.createElement('div');
            this.el.className = "pace pace-active";
            document.body.className = document.body.className.replace(/pace-done/g, '');
            document.body.className += ' pace-running';
            this.el.innerHTML = '<div class="pace-progress">\n  <div class="pace-progress-inner"></div>\n</div>\n<div class="pace-activity"></div>';
            if (targetElement.firstChild != null) {
                targetElement.insertBefore(this.el, targetElement.firstChild);
            } else {
                targetElement.appendChild(this.el);
            }
        }
        return this.el;
    };

    Bar.prototype.finish = function () {
        var el;
        el = this.getElement();
        el.className = el.className.replace('pace-active', '');
        el.className += ' pace-inactive';
        document.body.className = document.body.className.replace('pace-running', '');
        return document.body.className += ' pace-done';
    };

    Bar.prototype.update = function (prog) {
        this.progress = prog;
        return this.render();
    };

    Bar.prototype.destroy = function () {
        try {
            this.getElement().parentNode.removeChild(this.getElement());
        } catch (_error) {
            NoTargetError = _error;
        }
        return this.el = void 0;
    };

    Bar.prototype.render = function () {
        var el, key, progressStr, transform, _j, _len1, _ref2;
        if (document.querySelector(options.target) == null) {
            return false;
        }
        el = this.getElement();
        transform = "translate3d(" + this.progress + "%, 0, 0)";
        _ref2 = ['webkitTransform', 'msTransform', 'transform'];
        for (_j = 0, _len1 = _ref2.length; _j < _len1; _j++) {
            key = _ref2[_j];
            el.children[0].style[key] = transform;
        }
        if (!this.lastRenderedProgress || this.lastRenderedProgress | 0 !== this.progress | 0) {
            el.children[0].setAttribute('data-progress-text', "" + (this.progress | 0) + "%");
            if (this.progress >= 100) {
                progressStr = '99';
            } else {
                progressStr = this.progress < 10 ? "0" : "";
                progressStr += this.progress | 0;
            }
            el.children[0].setAttribute('data-progress', "" + progressStr);
        }
        return this.lastRenderedProgress = this.progress;
    };

    Bar.prototype.done = function () {
        return this.progress >= 100;
    };

    return Bar;

})();
$(".hamburger").click('click', function () {
    $(this).toggleClass("is-active");
});
$(function () {


    var dropdown = document.getElementsByClassName("dropdown-btn");
    var i;

    for (i = 0; i < dropdown.length; i++) {
        dropdown[i].addEventListener("click", function () {
            this.classList.toggle("active");
            var dropdownContent = this.nextElementSibling;
            if (dropdownContent.style.display === "block") {
                dropdownContent.style.display = "none";
            } else {
                dropdownContent.style.display = "block";
            }
        });
    }


    $("#menu-toggle-2").click(function () {
        if ($(window).width() <= 768) {

            if ($('body').hasClass('sidebar-mobile-main')) {
                $('body').removeClass('sidebar-mobile-main');
            }
            else {
                $('body').addClass('sidebar-mobile-main');
            }

        } else {

            if ($('body').hasClass('sidebar-xs')) {
                $('body').removeClass('sidebar-xs');
            }
            else {
                $('body').addClass('sidebar-xs');
            }
        }
    });



    $(window).on('resize', function () {
        setTimeout(function () {

            if ($(window).width() <= 768) {
                $('body').addClass('sidebar-xs-indicator');

            }
            else {
                $('body').removeClass('sidebar-xs-indicator');
            }
        }, 100);
    }).resize();


});

$("input[attr='numonly']").on('input', function (e) {
    $(this).val($(this).val().replace(/[^0-9]/g, ''));
});

$("input[attr='decimalonly']").on('keyup', function (e) {
    var num = $(this).attr("decimalPlaces").toString();
    var regex = new RegExp("^\\d{0,9}(\\.\\d{0," + num + "})?$");
    debugger;
    if (!regex.test(this.value)) {
        this.value = this.value.substring(0, this.value.length - 1);
    }
});

// Custom Application Scripts
function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [month, day, year,].join('/');
}
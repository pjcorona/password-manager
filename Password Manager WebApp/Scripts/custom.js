$(function () {

    //position the footer
    $('.container').css('min-height', $(window).height() - ($('.my-footer').height() * 2) - ($('.navbar').height() * 2));

    //toggle display of "showing results for '...'"
    if (typeof searchkeyword !== 'undefined') {
        if (searchkeyword != "") {
            $('.my-result-message').removeClass('invisible');
            $('.my-result-message').addClass('visible');
        }
    }

    //enable tooltips
    $('[data-toggle="tooltip"]').tooltip();

    //get account data and open modal    
    $(document).on("click", ".viewModalButton", function () {
        var id = $(this).attr('data-id');
        $.ajax({
            type: "GET",
            url: '/Account/ViewDetails',
            contentType: "application/json; charset=utf-8",
            data: { "id": id },
            datatype: "json",
            success: function (data) {
                $('#modalDynamicContent').html(data);
                $('#dynamicModal').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    
    //hover of password mask
    $(document).on("mouseenter", ".password-mask", function () {
        $(this).text($("#password-value").val());
        $('[data-toggle="tooltip"]').tooltip('update');
    }).on("mouseleave", ".password-mask", function () {
        $(this).text($("#password-value").val().replace(/./g, '*'));
    });

    //copy password value
    $(document).on('click', ".password-mask", function () {
        var passwordContainer = $("#password-value");
        passwordContainer.focus();
        passwordContainer.select();
        document.execCommand('copy');
    });

    //toggle password visibility
    $("#show-hide-password .my-toggle").on('click', function (event) {
        event.preventDefault();
        if ($('#show-hide-password input').attr("type") == "password") {
            $('#show-hide-password input').attr('type', 'text');
            $('#show-hide-password i').removeClass("fa-eye");
            $('#show-hide-password i').addClass("fa-eye-slash");
        }
        else if ($('#show-hide-password input').attr("type") == "text") {
            $('#show-hide-password input').attr('type', 'password');
            $('#show-hide-password i').removeClass("fa-eye-slash");
            $('#show-hide-password i').addClass("fa-eye");
        }
    });
    
    //function for checkbox toggle
    function checkBoxToggle() {
        if ($('#myCheckBox').is(':checked')) {
            var passwordTexboxValue = $('.passwords-entry').val();
            if (passwordTexboxValue == 'default') {
                $('.passwords-entry').val('');
            }
            $('.password-inputs').slideDown('fast', 'swing');
        }
        else {
            $('.password-inputs').slideUp('fast', 'swing', function() {
                $('.passwords-entry').val('default');
            });
            
        }
    }

    //edit details: checkbox toggle
    $(document).on('change', '#myCheckBox', function () {
        checkBoxToggle();
    });

    //edit details: checkbox toggle - on load
    $(window).on('load', function () {
        checkBoxToggle();
    });

});





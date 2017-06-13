$(document).ready(
    $(function() {
        $("#slider").slider({
            max: 10,
            min: 1,
            step: 1,
            slide: function(event, ui) {
                $("#sliderValue").val(ui.value);
            }
        });
        $("#sliderValue").val($("#slider").slider("value"));
    })

);

$(function() {
    $(".datepicker").datepicker({
        changeMonth: true,
        changeYear: true,
        showOn: "focus",
        showAnim: 'slideDown'
    });
});

$("#anim").change(function() {
    $("#datepicker").datepicker("option", "showAnim", $(this).val());
});


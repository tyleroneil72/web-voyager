$("#ajaxSearchButtonCar").click(function (e) {
  e.preventDefault();
  var searchString = $("#ajaxSearchStringCar").val();
  $.ajax({
    url: "/Car/AjaxSearch",
    type: "GET",
    data: { searchString: searchString },
    success: function (result) {
      $(".table-responsive").hide();
      $("#ajaxSearchResults").html(result);
    },
  });
});

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

$("#ajaxSearchButtonHotel").click(function (e) {
  e.preventDefault();
  var searchString = $("#ajaxSearchStringHotel").val();
  $.ajax({
    url: "/Hotel/AjaxSearch",
    type: "GET",
    data: { searchString: searchString },
    success: function (result) {
      $(".table-responsive").hide();
      $("#ajaxSearchResults").html(result);
    },
  });
});

$("#ajaxSearchButtonFlight").click(function (e) {
  e.preventDefault();
  var searchString = $("#ajaxSearchStringFlight").val();
  $.ajax({
    url: "/Flight/AjaxSearch",
    type: "GET",
    data: { searchString: searchString },
    success: function (result) {
      $(".table-responsive").hide();
      $("#ajaxSearchResults").html(result);
    },
  });
});

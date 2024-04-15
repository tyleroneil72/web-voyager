$("#ajaxSearchButtonCar").click(function (e) {
  e.preventDefault();
  var searchString = $("#ajaxSearchStringCar").val();
  $("#loader").show();

  setTimeout(function () {
    $("#loader").hide();
  }, 1000);

  $.ajax({
    url: "/Car/AjaxSearch",
    type: "GET",
    data: { searchString: searchString },
    success: function (result) {
      setTimeout(function () {
        $(".table-responsive").hide();
        $(".genericSearch").hide();
        $("#ajaxSearchResults").html(result);
      }, 1000);
    },
    error: function () {
      setTimeout(function () {
        $("#loader").hide();
      }, 1000);
    },
  });
});

$("#ajaxSearchButtonHotel").click(function (e) {
  e.preventDefault();
  var searchString = $("#ajaxSearchStringHotel").val();
  $("#loader").show();

  setTimeout(function () {
    $("#loader").hide();
  }, 1000);

  $.ajax({
    url: "/Hotel/AjaxSearch",
    type: "GET",
    data: { searchString: searchString },
    success: function (result) {
      setTimeout(function () {
        $(".table-responsive").hide();
        $(".genericSearch").hide();
        $("#ajaxSearchResults").html(result);
      }, 1000);
    },
    error: function () {
      setTimeout(function () {
        $("#loader").hide();
      }, 1000);
    },
  });
});

$("#ajaxSearchButtonFlight").click(function (e) {
  e.preventDefault();
  var searchString = $("#ajaxSearchStringFlight").val();
  $("#loader").show();

  setTimeout(function () {
    $("#loader").hide();
  }, 1000);

  $.ajax({
    url: "/Flight/AjaxSearch",
    type: "GET",
    data: { searchString: searchString },
    success: function (result) {
      setTimeout(function () {
        $(".table-responsive").hide();
        $(".genericSearch").hide();
        $("#ajaxSearchResults").html(result);
      }, 1000);
    },
    error: function () {
      setTimeout(function () {
        $("#loader").hide();
      }, 1000);
    },
  });
});

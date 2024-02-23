$(document).on("click", ".not-implemented", function () {
  alert("Not implemented");
});

$(document).ready(function () {
  let lastScrollTop = 0;

  function getNavbarHeight() {
    return $(".navbar").outerHeight();
  }

  function adjustNavbar() {
    let currentScrollTop = $(window).scrollTop();
    let navbarHeight = getNavbarHeight(); // Dynamically get the navbar height

    if (currentScrollTop > lastScrollTop) {
      // Scrolling down
      $(".navbar").css({
        position: "fixed",
        top: -navbarHeight + "px", // Use the dynamically obtained height
        width: "100%",
      });
      $(".navbar-placeholder").height(navbarHeight).show(); // Adjust placeholder height dynamically
    } else {
      // Scrolling up or at the top
      $(".navbar").css({ position: "fixed", top: "0", width: "100%" });
      if (currentScrollTop <= 0) {
        // At the top, make the navbar static again and hide placeholder
        $(".navbar").css({ position: "static" });
        $(".navbar-placeholder").hide();
      }
    }
    lastScrollTop = currentScrollTop;
  }

  // Insert a placeholder to maintain layout space when the navbar is fixed
  $(".navbar").after('<div class="navbar-placeholder"></div>');

  $(window).scroll(adjustNavbar);

  adjustNavbar(); // Use the function to adjust navbar on load/refresh

  // Recalculate navbar height on window resize
  $(window).resize(function () {
    adjustNavbar();
  });
});

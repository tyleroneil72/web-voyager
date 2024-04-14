function loadReviews(reviewingId, type) {
  console.log(`Loading reviews for ${type} with ID: ${reviewingId}`); // Debug
  $.ajax({
    url: `/TravelServices/Review/GetReviews?reviewingId=${reviewingId}&type=${type}`,
    method: "GET",
    success: function (data) {
      var reviewsHtml = "";
      data.forEach((review) => {
        reviewsHtml += '<div class="review">';
        reviewsHtml += `<p>${review.content}</p>`;
        reviewsHtml += `<span>Posted on: ${new Date(
          review.createdDate
        ).toLocaleString()}</span>`;
        reviewsHtml += "</div>";
      });
      $(`#${type.toLowerCase()}ReviewsList`).html(reviewsHtml);
    },
    error: function (xhr, status, error) {
      console.error(`Failed to load reviews for ${type}: ${error}`); // Debug
    },
  });
}

$(document).ready(function () {
  ["Flight", "Car", "Hotel"].forEach((type) => {
    const id = $(
      `#${type.toLowerCase()}Comments input[name="${type}Id"]`
    ).val();
    console.log(`Setting up ${type} with ID: ${id}`); // Debug
    if (id) {
      loadReviews(id, type);
      $(`#${type.toLowerCase()}AddReviewForm`).submit(function (e) {
        e.preventDefault();
        var formData = {
          ReviewingId: id,
          Content: $(
            `#${type.toLowerCase()}Comments textarea[name="Content"]`
          ).val(),
          ReviewableType: type,
        };
        console.log(`Submitting review for ${type}:`, formData); // Debug
        $.ajax({
          url: "/TravelServices/Review/AddReview",
          method: "POST",
          contentType: "application/json",
          data: JSON.stringify(formData),
          success: function (response) {
            if (response.success) {
              $(`#${type.toLowerCase()}Comments textarea[name="Content"]`).val(
                ""
              );
              loadReviews(id, type);
            } else {
              alert(response.message);
            }
          },
          error: function (xhr, status, error) {
            alert("Error: " + error);
          },
        });
      });
    }
  });
});

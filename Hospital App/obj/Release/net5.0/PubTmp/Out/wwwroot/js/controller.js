$(document).ready(function () {
            $("#cityDropdown").change(function () {
                var selectedCityId = $(this).val();
                if (selectedCityId) {
                    $.ajax({
                        url: "/Home/GetSuburbsByCity", // Adjust the URL as needed
                        type: "GET",
                        data: { cityId: selectedCityId },
                        success: function (data) {
                            $("#SuburbID").empty();
                            $("#SuburbID").append("<option value=''>Select Suburb</option>");
                            $.each(data, function (index, suburb) {
                                $("#SuburbID").append("<option value='" + suburb.SuburbID + "'>" + suburb.SuburbName + "</option>");
                            });
                        }
                    });
                } else {
                    $("#SuburbID").empty();
                    $("#SuburbID").append("<option value=''>Select Suburb</option>");
                }
            });
});
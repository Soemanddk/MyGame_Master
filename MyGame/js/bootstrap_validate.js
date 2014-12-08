$(document).ready(function () {

    // Bootstrap Required.
    // put a <div class="form"> around your so called form.
    // add class="validate" on your submit link
    // add data-regex="" with your desired regex expression or leave blank or write empty, in the desired input/textarea fields.
    // if data-regex aint set inside field then it will not validate on it.
    // if data-regex="" left blank then it validate with presettings.
    // if theres written empty inside data-regex like this data-regex="empty", then it will only validate if field is empty.
    // a custom regex to validate on is given like this data-regex="^((([\w]+\.[\w]+)+)|([\w]+))@(([\w]+\.)+)([A-Za-z]{1,3})$"
    $(function () {

        $(".form a.validate").click(function (e) {
            ValidateForm($(this).parent(), e);
        });

        $(".form").keyup(function (e) {
            ValidateForm($(this).parent().parent(), "");
        });

        function ValidateForm(Form, Event) {

            var check = true;

            // Removes feedback classes and spans
            Form.find("div.has-feedback").removeClass("has-error has-success has-feedback");
            Form.find("span.form-control-feedback").remove();

            // Foreach Input Field inside .form
            Form.find("input").each(function () {
                var valid = true;

                // Checking if data-regex is set on input field.
                if ($(this).data("regex") != undefined) {
                    var data_regex = "";

                    if ($(this).data("regex") != "") {
                        // Checking if a pre or not regularexpression is set on input field
                        data_regex = BuildDataRegex($(this).data("regex"));
                    }
                    else {
                        switch ($(this).attr("type")) {
                            case "text":
                                data_regex = /.{3,}$/; // Atleast 4 charecters
                                break;
                            case "password":
                                data_regex = /^(?=.*[A-Z])(?=.*[0-9]).{6,}$/; // 1 Uppercase, 1 number, minimum 6 charecters long
                                break;
                            case "email":
                                data_regex = /^((([\w]+\.[\w]+)+)|([\w]+))@(([\w]+\.)+)([A-Za-z]{1,3})$/g; // Something @ Something . Between 1 & 3 Letters
                                break;
                            case "color":
                                data_regex = /\b[0-9A-F]{6}\b/gi; // Must be #, Must be 6 charecters
                                break;
                            case "date":
                                data_regex = /^[0-9]{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])/; // Must be 4 numbers - Between 1 & 2 numbers - Between 1 & 2 numbers
                                break;
                            case "month":
                                data_regex = /^(\d{4})-(\d{1,2})$/; // Must be 4 numbers - Between 1 & 2 numbers
                                break;
                            case "number":
                                data_regex = /^\d*(?:\.\d{1,2})?$/; // Only Numbers
                                break;
                        }
                    }

                    var test = TestRegexAddClasses($(this), data_regex);
                    if (test == false)
                    {
                        check = test;
                    }
                }
            });

            Form.find("textarea").each(function ()
            {

                var valid = true;

                // Checking if data-regex is set on input field.
                if ($(this).data("regex") != undefined)
                {
                    var data_regex = "";

                    if ($(this).data("regex") != "")
                    {
                        // Checking if a pre or not regularexpression is set on input field
                        data_regex = BuildDataRegex($(this).data("regex"));
                    }
                    else {
                        data_regex = /.{40,}$/; // Atleast 40 charecters
                    }

                    var test = TestRegexAddClasses($(this), data_regex);
                    if (test == false) {
                        check = test;
                    }
                }
            });

            function BuildDataRegex(DataRegex)
            {
                var data_regex;

                switch (DataRegex) {
                    case "empty":
                        data_regex = "";
                        break;
                    default:
                        data_regex = DataRegex;
                }

                return data_regex;
            }
            function TestRegexAddClasses(Field, DataRegex)
            {
                var check = true;
                // Checking regularexpression with the expression in data-regex.
                var regex = new RegExp(DataRegex);
                valid = regex.test(Field.val()); // Returns false if it fails.

                // Checking if input field is not empty and valid is true
                if (Field.val() != "" && valid)
                {
                    // Success
                    Field.parent().addClass("has-success has-feedback");
                    Field.after("<span class='glyphicon glyphicon-ok form-control-feedback'></span>");
                }
                else {
                    // Failure
                    Field.parent().addClass("has-error has-feedback");
                    Field.after("<span class='glyphicon glyphicon-remove form-control-feedback'></span>");
                    check = false;
                }

                var SpanHeight = Field.outerHeight();

                Field.parent().find("span.form-control-feedback").css({ "top": (Field.offset().top - Field.parent().offset().top + 1) + "px", "height": SpanHeight + "px", "line-height": SpanHeight + "px" });

                return check;
            }

            if (check) {
                Form.find("a.validate").removeClass().addClass("validate btn btn-success");
            }
            else
            {
                Form.find("a.validate").removeClass().addClass("validate btn btn-danger");
                if (Event != "")
                {
                    Event.preventDefault();
                }
            }
        }
    });
});
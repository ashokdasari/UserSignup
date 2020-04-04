let SignupForm = function () {
    let initialize = function () {
        $('form').on('submit', (event) => {
            event.preventDefault();
            event.stopPropagation();
            $('form').removeClass('was-validated');
            if (!validateForm()) {
                return;
            }
            let signModel = getSignupModel();
            fetch(globalSettings.apiUrl + 'user/signup', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(signModel)
            })
                .then(response => response.json())
                .then((res) => {
                    if (res && res.success) {
                        window.location = $('form')[0].action;
                    } else if (res.unmetExpectations && res.unmetExpectations.length > 0) {
                        setValidations(res.unmetExpectations);
                    }
                })
                .catch(error => console.error('Error Occured. ', error));
        });
    };

    let validateForm = function () {
        let email = $('#Email').val();
        let password = $('#Password').val();
        let validations = [];
        if (email === '') {
            validations.push({
                key: 'Email',
                message: "Required"
            });
        }
        if (password === '') {
            validations.push({
                key: 'Password',
                message: "Required"
            });
        }
        setValidations(validations);       
        return validations.length === 0;
    };

    let setValidations = function (validations) {
        // clear the existing validations
        $('.invalid-feedback').html('');
        let $formControls = $('.form-control');
        if ($formControls.length > 0) {
            for (let i = 0; i < $formControls.length; i++) {
                $formControls[i].setCustomValidity('');
            }
        }

        if (validations && validations.length) {
            let validationSummary = '';
            validations.forEach(validation => {
                if (!validation.key) {
                    validationSummary = validationSummary + ' ' + validation.message;
                } else {
                    $(`#${(validation.key)}Feedback`).html(validation.message);
                    $(`#${(validation.key)}Feedback`).show();
                    $(`#${(validation.key)}`)[0].setCustomValidity(validation.message);
                }
            });
            if (validationSummary) {
                $('#validationSummary').html(validationSummary).show();
            } else {
                $('#validationSummary').hide();
            }
        }
        $('form').addClass('was-validated');
    };

    $('#chkShowPassword').change(function () {
        if (this.checked) {
            $('#Password').attr('type', 'text');
        } else {
            $('#Password').attr('type', 'password');
        }
    });
    let getSignupModel = () => {
        return {
            Email: $('#Email').val(),
            Password: $('#Password').val()
        };

    };

    return {
        initialize: initialize
    };
};
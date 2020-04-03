let SigninForm = function () {
    let initialize = function () {
        $('#SigninButton').on('click', (event) => {
            event.preventDefault();
            event.stopPropagation();
            let form = $('form');
            form.addClass('was-validated');
            form[0].checkValidity();
            if (!validateForm()) {
                return;
            }
            let signModel = getSigninModel();
            fetch(globalSettings.apiUrl + 'user/validate', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(signModel)
            })
                .then(response => response.json())
                .then((res) => {
                    if (res && res.success && res.userId) {
                        $('#UserId').val(res.userId);
                        $('#SubmitButton').click();
                    } else if (res.unmetExpectations && res.unmetExpectations.length > 0) {
                        event.preventDefault();
                        setValidations(res.unmetExpectations);
                    }
                })
                .catch(error => console.error('Error Occurred. ', error));
        });
    };

    let validateForm = function () {
        let email = $('#Email').val();
        let password = $('#Password').val();
        let verificationCode = $('#VerificationCode').val();
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

        if (verificationCode === '') {
            validations.push({
                key: 'VerificationCode',
                message: "Required"
            });
        }
        setValidations(validations);
        return validations.length === 0;
    };

    let setValidations = function (validations) {
        $('.invalid-feedback').html('');

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
    };

    $('#chkShowPassword').change(function () {
        if (this.checked) {
            $('#Password').attr('type', 'text');
        } else {
            $('#Password').attr('type', 'password');
        }
    });
    let getSigninModel = () => {
        return {
            Email: $('#Email').val(),
            Password: $('#Password').val(),
            VerificationCode: $('#VerificationCode').val()
        };

    };

    return {
        initialize: initialize
    };
};
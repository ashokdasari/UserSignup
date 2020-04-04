let ProfileForm = function () {
    let initialize = function () {
        $('#btnSave').on('click', (event) => {

            event.preventDefault();
            event.stopPropagation();
            $('form').removeClass('was-validated');
            if (!validateForm()) {
                return;
            }

            let profileModel = getProfileModel();
            fetch(globalSettings.apiUrl + 'user/save', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(profileModel)
            })
                .then(response => response.json())
                .then((res) => {
                    if (res && res.success) {
                        $('#btnSubmit').click();
                    } else if (res.unmetExpectations && res.unmetExpectations.length > 0) {
                        setValidations(res.unmetExpectations);
                    }
                })
                .catch(error => console.error('Error Occurred. ', error));
        });
    };

    let validateForm = function () {
        let fullName = $('#FullName').val();
        let contactNumber = $('#ContactNumber').val();
        let address = $('#Address').val();
        let validations = [];
        if (fullName === '') {
            validations.push({
                key: 'FullName',
                message: "Required"
            });
        }
        if (contactNumber === '') {
            validations.push({
                key: 'ContactNumber',
                message: "Required"
            });
        }

        if (address.trim() === '') {
            validations.push({
                key: 'Address',
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

    let getProfileModel = () => {
        return {
            FullName: $('#FullName').val(),
            ContactNumber: $('#ContactNumber').val(),
            Address: $('#Address').val(),
            SendPromotions: $('#SendPromotions').is(':checked'),
            UserId: parseInt($('#UserId').val())
        };

    };

    return {
        initialize: initialize
    };
};
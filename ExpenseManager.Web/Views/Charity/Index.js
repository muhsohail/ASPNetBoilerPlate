(function () {
    $(function () {

        var _roleService = abp.services.app.charity;
        var _$modal = $('#CharityModal');
        var _$form = _$modal.find('form');

        _$form.validate({
        });

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('.delete-charity').click(function () {
            var charityId = $(this).attr("data-charity-id");
            var charityName = $(this).attr('data-charity-name');

            deleteCharity(charityId, charityName);
        });

        $('.delete-bankdetails').click(function () {
            var bankdetailsId = $(this).attr("data-bankdetails-id");
            var bankdetailsName = $(this).attr('data-bankdetails-name');

            deleteBankdetails(bankdetailsId, bankdetailsName);
        });

        $('.undo-charity').click(function () {
            var charityId = $(this).attr("data-charity-id");
            var charityName = $(this).attr('data-charity-name');

            undoCharity(charityId, charityName);
        });

        $('.edit-charity').click(function (e) {
            var charityId = $(this).attr("data-charity-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Charity/EditCharity?charityId=' + charityId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#CharityEditModal div.modal-content').html(content);
                },
                error: function (e)
                {

                }
            });
        });

        $('.edit-bankdetails').click(function (e) {
            var bankdetailsId = $(this).attr("data-bankdetails-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'BankDetails/EditBankDetails?bankdetailsId=' + bankdetailsId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#BankDetailsEditModal div.modal-content').html(content);
                },
                error: function (e) {

                }
            });
        });

        function refreshList() {
            location.reload(true);
        }

        function deleteCharity(charityId, charityName) {

            var charityId = charityId;
            abp.message.confirm(
                "Remove exepnse entry '" + charityName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        //var charityId = charityId;

                        $.ajax({
                            url: abp.appPath + 'Charity/DeleteCharity?charityId=' + charityId,
                            type: 'POST',
                            contentType: 'application/html',
                            success: function (content) {
                                refreshList();
                            },
                            error: function (e) { }
                        });
                    }
                }
            );
        }

        function deleteBankdetails(bankdetailsId, bankdetailsName) {

            var charityId = charityId;
            abp.message.confirm(
                "Remove bankdetails entry '" + bankdetailsName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        //var charityId = charityId;

                        $.ajax({
                            url: abp.appPath + 'BankDetails/DeleteBankDetails?BankDetailsId=' + bankdetailsId,
                            type: 'POST',
                            contentType: 'application/html',
                            success: function (content) {
                                refreshList();
                            },
                            error: function (e) { }
                        });
                    }
                }
            );
        }

        function undoCharity(charityId, charityName) {

            var charityId = charityId;
            abp.message.confirm(
                "Undo deleted exepnse entry '" + charityName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        //var charityId = charityId;

                        $.ajax({
                            url: abp.appPath + 'Charity/UndoCharity?charityId=' + charityId,
                            type: 'POST',
                            contentType: 'application/html',
                            success: function (content) {
                                refreshList();
                            },
                            error: function (e) { }
                        });
                    }
                }
            );
        }
    });
})();
(function () {
    $(function () {

        var _$modal = $('#LoanCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate({
        });

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('.delete-loan').click(function () {
            var loanId = $(this).attr("data-loan-id");
            var loanName = $(this).attr('data-loan-name');

            deleteLoan(loanId, loanName);
        });


        $('.edit-loan').click(function (e) {
            var loanId = $(this).attr("data-loan-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Loan/EditLoan?loanId=' + loanId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#LoanEditModal div.modal-content').html(content);
                },
                error: function (e) {

                }
            });
        });


        function refreshList() {
            location.reload(true);
        }

        function deleteLoan(loanId, loanName) {

            var loanId = loanId;
            abp.message.confirm(
                "Remove pension receivable entry '" + loanName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        $.ajax({
                            url: abp.appPath + 'Loan/DeleteLoan?loanId=' + loanId,
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

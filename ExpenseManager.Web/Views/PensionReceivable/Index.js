(function () {
    $(function () {

        var _$modal = $('#PensionReceivableCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate({
        });

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('.delete-pensionReceivable').click(function () {
            var pensionReceivableId = $(this).attr("data-pensionReceivable-id");
            var pensionReceivableName = $(this).attr('data-pensionReceivable-name');

            deletePensionReceivable(pensionReceivableId, pensionReceivableName);
        });


        $('.edit-pensionReceivable').click(function (e) {
            var pensionReceivableId = $(this).attr("data-pensionReceivable-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'PensionReceivable/EditPensionReceivable?pensionReceivableId=' + pensionReceivableId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#PensionReceivableEditModal div.modal-content').html(content);
                },
                error: function (e) {

                }
            });
        });


        function refreshList() {
            location.reload(true);
        }

        function deletePensionReceivable(pensionReceivableId, pensionReceivableName) {

            var pensionReceivableId = pensionReceivableId;
            abp.message.confirm(
                "Remove pension receivable entry '" + pensionReceivableName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        $.ajax({
                            url: abp.appPath + 'PensionReceivable/DeletePensionReceivable?pensionReceivableId=' + pensionReceivableId,
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

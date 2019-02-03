(function () {
    $(function () {

        var _roleService = abp.services.app.pension;
        var _$modal = $('#PensionCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate({
        });

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('.delete-pension').click(function () {
            var pensionId = $(this).attr("data-pension-id");
            var pensionName = $(this).attr('data-pension-name');

            deletePension(pensionId, pensionName);
        });


        $('.edit-pension').click(function (e) {
            var pensionId = $(this).attr("data-pension-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Pension/EditPension?pensionId=' + pensionId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#PensionEditModal div.modal-content').html(content);
                },
                error: function (e) {

                }
            });
        });


        function refreshList() {
            location.reload(true);
        }

        function deletePension(pensionId, pensionName) {

            var pensionId = pensionId;
            abp.message.confirm(
                "Remove pension entry '" + pensionName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        $.ajax({
                            url: abp.appPath + 'Pension/DeletePension?pensionId=' + pensionId,
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

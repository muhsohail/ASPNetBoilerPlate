(function () {
    $(function () {

        var _roleService = abp.services.app.onus;
        var _$modal = $('#OnusCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate({
        });

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('.delete-onus').click(function () {
            var onusId = $(this).attr("data-onus-id");
            var onusName = $(this).attr('data-onus-name');

            deleteOnus(onusId, onusName);
        });


        $('.edit-onus').click(function (e) {
            var onusId = $(this).attr("data-onus-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Onus/EditOnus?onusId=' + onusId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#OnusEditModal div.modal-content').html(content);
                },
                error: function (e) {

                }
            });
        });


        $('.undo-onus').click(function () {
            var onusId = $(this).attr("data-onus-id");
            var onusName = $(this).attr('data-onus-name');

            undoOnus(onusId, onusName);
        });


        function refreshList() {
            location.reload(true);
        }

        function deleteOnus(onusId, onusName) {

            var onusId = onusId;
            abp.message.confirm(
                "Remove onus entry '" + onusName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        $.ajax({
                            url: abp.appPath + 'Onus/DeleteOnus?onusId=' + onusId,
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


        function undoOnus(onusId, onusName) {

            var onusId = onusId;
            abp.message.confirm(
                "Undo onus entry '" + onusName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        $.ajax({
                            url: abp.appPath + 'Onus/UndoOnus?onusId=' + onusId,
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

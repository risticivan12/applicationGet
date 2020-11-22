const connection = new signalR.HubConnectionBuilder()
    .withUrl("/tableData")
    .build();

$(document).ready(
    $.noConflict(),
    setButtonsExam(),
    setButtonsSubject(),
    setButtonsStudent()
)


function setButtonsExam() {
    var addExamButton = $('#addExam');
    var indexNumberInput = $('#indexNumber');
    var cancelExamButton = $('#cancelExam');
    var tableExams = $('#table_exams').DataTable(
        {
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [3],
                    "visible": false,
                    "searchable": false
                }
            ]
        });

    $('#examForm tbody').on('click', '.buttonTableExamUpdate', function () {
        var data = tableExams.row($(this).parents('tr')).data();
        $('#examId').val(data[0]);
        $('#indexNumber').val(data[1]);
        $("#subjectOption :selected").text(data[2]);
        $("#subjectOption :selected").val(data[3]);
        $('#examMark').val(data[4]);
        $('#examDate').val(data[5]);
        setButtonsStyle('hidden', 'none');
        setFieldsForUpdate();
    });

    $('#examForm tbody').on('click', '.buttonTableExamDelete', function () {
        var data = tableExams.row($(this).parents('tr')).data();
        var actionUrl = window.location.origin + $('.buttonTableExamDelete').attr('formaction');
        var formData = new FormData();
        formData.append('ExamId', data[0]);
        formData.append('IndexNumber', data[1]);
        formData.append('SubjectId', data[3]);
        setFieldsAfterUpdate();
        setButtonsStyle("hidden", "none");
        $.ajax({
            cache: false,
            contentType: false,
            processData: false,
            method: 'DELETE',
            type: 'DELETE',
            url: actionUrl,
            data: formData,
            success: function (data) {
                if (data == null) {
                    alert('Doslo je do greske prilikom brisanja! ');
                } else {
                    console.log(data);

                }
            }
        });
    });

    deleteRowFromTable('#table_exams tbody', '#table_exams', '.buttonTableExamDelete');

    indexNumberInput.keyup(function () {
        var indexNumber = indexNumberInput.val();
        var actionUrl = window.location.origin + '/Student/Suggested';
        var formData = new FormData();
        formData.append("IndexNumber", indexNumber);
        console.log(actionUrl);
        if (indexNumber !== "") {
            $.ajax({
                cache: false,
                contentType: false,
                processData: false,
                method: 'PUT',
                type: 'PUT',
                url: actionUrl,
                data: formData,
                success: function (data) {
                    if (data.length !== 0) {
                        console.log(data);
                        var suggests = "<a title=\"\"" + data[0] + "\"\ href=\'#'\" class = \"\suggestValue\"\ >" + data[0] + "</a><br>";
                        for (var i = 1; i < data.length; i++) {
                            suggests += "<a title=\"\"" + data[i] + "\"\ href =\'#'\" class = \"\suggestValue\"\>" + data[i] + "</a>";
                        }

                        $("#suggestedStudents").html(suggests);
                        $("#suggestedStudents").show();
                        setSubjectsByChoosenIndexNumber();

                    } else {
                        $("#suggestedStudents").html("Student sa ovim brojem indeksa ne postoji u bazi!");
                    }
                }

            });

        } else {
            $("#suggestedStudents").html("Student sa ovim brojem indeksa ne postoji u bazi!");
            $("#suggestedStudents").hide();
        }

    });


    function setSubjectsByChoosenIndexNumber() {
        $('.suggestValue').click(function (event) {
            var indexNumber = event.target.innerHTML;
            var actionUrl = window.location.origin + '/Subject/List';
            var formData = new FormData();
            formData.append("IndexNumber", indexNumber);
            indexNumberInput.val(indexNumber);
            indexNumberInput.attr('disabled', 'disabled');
            $("#suggestedStudents").hide();

            $.ajax({
                cache: false,
                contentType: false,
                processData: false,
                method: 'PUT',
                type: 'PUT',
                url: actionUrl,
                data: formData,
                success: function (data) {
                    populateDropDown(data);
                    setButtonsStyle("visible", "inline-block");
                }

            });
        });
    }

    function setFieldsAfterUpdate() {
        document.getElementById("updateExam").style.visibility = "hidden";
        document.getElementById("updateExam").style.display = "none";
        $('#indexNumber').removeAttr('disabled');
        $('#subjectOption').removeAttr('disabled');
        document.getElementById("examForm").reset();
    }


    function checkInputValuesExam() {
        if ($('#examMark').val().trim() === '' || $('#examDate').val().trim() === '') {
            $('#errorExam').html('<font style="color:red">Sva polja su obavezna!<font>');
            return false;
        }
        if (parseInt($('#examMark').val()) < 6 || parseInt($('#examMark').val()) > 10) {
            $('#errorExam').html('<font style="color:red">Ocena mora biti u intervalu od 6 do 10 !<font>');
            return false;
        }

        $('#errorExam').html('');
        return true;
    }

    $('#updateExam').click(function (event) {
        if (!checkInputValuesExam()) {
            return;
        }
        var actionUrl = window.location.origin + $('#updateExam').attr('formaction');
        var formData = new FormData();
        formData.append("examId", $('#examId').val());
        formData.append("examMark", $('#examMark').val() + '');
        formData.append("examDate", $('#examDate').val());
        setFieldsAfterUpdate();

        $.ajax({
            cache: false,
            contentType: false,
            processData: false,
            method: 'PUT',
            type: 'PUT',
            url: actionUrl,
            data: formData,
            success: function (data) {
                console.log(data);
                location.reload();
            }

        });

    });

    function setFieldsForUpdate() {
        $('#indexNumber').attr('disabled', 'disabled');
        $('#subjectOption').attr('disabled', 'disabled');
        document.getElementById("updateExam").style.visibility = "visible";
        document.getElementById("updateExam").style.display = "inline-block";
    }

    function populateDropDown(data) {
        var dropdown = $("#subjectOption");
        dropdown.empty();
        for (var i = 0; i < data.length; i++) {
            dropdown.append($("<option />").val(data[i].subjectId).text(data[i].title));
        }
    }

    function setButtonsStyle(btnVisibility, btnDisplay) {
        document.getElementById("cancelExam").style.visibility = btnVisibility;
        document.getElementById("cancelExam").style.display = btnDisplay;
        document.getElementById("addExam").style.visibility = btnVisibility;
        document.getElementById("addExam").style.display = btnDisplay;
    }

    cancelExamButton.click(function (event) {
        setButtonsStyle("disabled", "none");
        document.getElementById("examForm").reset();
        indexNumberInput.removeAttr('disabled');
    });

    addExamButton.click(function (event) {
        if (!checkInputValuesExam()) {
            return;
        }
        var actionUrl = window.location.origin + "/Exam/Save";
        const indexNumber = $('#indexNumber').val();
        const subjectId = $("#subjectOption :selected").val();
        const mark = $('#examMark').val();
        const date = $('#examDate').val();



        document.getElementById("examForm").reset();
        var formData = new FormData();
        setButtonsStyle("disabled", "none");
        formData.append("IndexNumber", indexNumber);
        formData.append("SubjectId", subjectId);
        formData.append("Mark", mark);
        formData.append("Date", date);
        $('#indexNumber').removeAttr('disabled');

        $.ajax({
            cache: false,
            contentType: false,
            processData: false,
            method: 'POST',
            type: 'POST',
            url: actionUrl,
            data: formData,
            success: function (data) {
                console.log(data);
                sendMessageSignalR(data);
            }

        });
    });

    //start listening

    connection.start().catch(error => console.log(error));

    //send message
    function sendMessageSignalR(data) {
        connection.invoke("UpdateTable", data).catch(error => console.log(error));
        event.preventDefault();
    }


    //recive message

    connection.on("UpdatedData", (exam) => {
        console.log('Uslo je u recieve message');
        console.log(exam);
        addExamToTable(exam);
    });

    function addExamToTable(data) {
        console.log('Dodavanje u tabelu');
        console.log(data);
        const idData = data.examId + "," + data.student.indexNumber + "," + data.student.studentId + "," + data.subject.subjectId + "," + data.subject.title.split(" ").join(".") + "," + data.mark + "," + data.examDate;
        console.log(idData);
        $("#table_exams").DataTable().row.add([
            data.examId,
            data.student.indexNumber,
            data.subject.title,
            data.subject.subjectId,
            data.mark,
            data.examDate.split('T')[0],
            "<button type =\"button\"  class = \"btn btn-success buttonTableExamUpdate\" > Izmeni</button > ",
            "<button type =\"button\" asp-action=\"DeleteExam\" asp-controller=\"Exam\"  class = \"btn btn-danger buttonTableExamDelete\"> Obriši</button > "
        ]).draw();
        $('#examDate').val('').datepicker('update');
    }

}

function setButtonsSubject() {
    var addSubjectButton = $('#addSubject');
    var tableEmployee = $('#table_studentsBySubjects');
    var listElement = $('.subjectName');
    var deleteSubjectButton = $('#deleteSubject');
    var updateSubjectButton = $('#updateSubject');
    var saveUpdatedSubjectButton = $('#saveUpdatedSubject');
    var prepareForAddButton = $('#prepareForAdd');
    tableEmployee.DataTable();

    addActionOnSubjectNameButton(listElement);

    addSubjectButton.click(function (event) {
        if (!checkInputValuesForSubject()) {
            return;
        }
        var actionUrl = window.location.origin + addSubjectButton.attr('formaction');
        var subjectTitle = $('#subjectTitleInput').val();
        var formData = new FormData();
        formData.append("Title", subjectTitle);
        $('#subjectTitleInput').val('');

        $.ajax({
            cache: false,
            contentType: false,
            processData: false,
            method: 'POST',
            type: 'POST',
            url: actionUrl,
            data: formData,
            success: function (data) {
                if (data != null) {
                    setListAdd(data);
                    console.log(data);
                } else {
                    alert('Doslo je greske prilikom cuvanja predmeta, pokusajte ponovo!');
                }
            }

        });
    });

    function checkInputValuesForSubject() {
        if ($('#subjectTitleInput').val().trim() === '') {
            $('#errorSubject').html('<font style="color:red">Sva polja su obavezna!<font>');
            return false;
        }
        $('#errorSubject').html('');
        return true;
    }

    saveUpdatedSubjectButton.click(function () {
        if (!checkInputValuesForSubject()) {
            return;
        }
        var actionUrl = window.location.origin + saveUpdatedSubjectButton.attr('formaction');
        var subjectId = $('#subjectId').val();
        var subjectTitle = $('#subjectTitleInput').val();
        var formData = new FormData();
        formData.append("SubjectId", subjectId);
        formData.append("Title", subjectTitle);

        $.ajax({
            cache: false,
            contentType: false,
            processData: false,
            method: 'PUT',
            type: 'PUT',
            url: actionUrl,
            data: formData,
            success: function (data) {
                console.log(data);
                location.reload();
            }

        });
    });

    updateSubjectButton.click(function (event) {
        var subjectTitle = $('#subjectTitleInput').val();
        setButtonsSave();
        $('#subjectTitleInput').val(subjectTitle);
        document.getElementById("addSubject").style.visibility = "hidden";
        document.getElementById("addSubject").style.display = "none";
        document.getElementById("saveUpdatedSubject").style.visibility = "visible";
        document.getElementById("saveUpdatedSubject").style.display = "inline-block";
    });

    function addActionOnSubjectNameButton(subjectNameButton) {
        subjectNameButton.click(function (event) {
            var inputTitle = $('#subjectTitleInput');
            var subjectId = $('#subjectId');
            inputTitle.val(event.currentTarget.name);
            inputTitle.attr('disabled', 'disabled');
            subjectId.val(event.currentTarget.id);
            setButtonsUpdateAndDelete();
            getListOfStudentsBySubjectId();
        });
    }

    function getListOfStudentsBySubjectId() {
        var actionUrl = window.location.origin + '/Subject/GetListOfStudentsBySubjectId';
        var subjectId = $('#subjectId').val();
        var formData = new FormData();
        formData.append("SubjectId", subjectId);

        $.ajax({
            cache: false,
            contentType: false,
            processData: false,
            method: 'PUT',
            type: 'PUT',
            url: actionUrl,
            data: formData,
            success: function (data) {
                console.log(data);
                setValuesToTable(data);
            }

        });
    }

    function setValuesToTable(data) {
        $("#table_studentsBySubjects").DataTable().clear().draw();
        for (var i = 0; i < data.length; i++) {
            $("#table_studentsBySubjects").DataTable().row.add([
                data[i].indexNumber,
                data[i].firstName,
                data[i].lastName,
                data[i].city,
                data[i].examDate.split('T')[0],
                data[i].mark
            ]).draw();
        }
    }

    prepareForAddButton.click(function (event) {
        setButtonsSave();
    });


    function setButtonsUpdateAndDelete() {
        document.getElementById("deleteSubject").style.visibility = "visible";
        document.getElementById("updateSubject").style.visibility = "visible";
        document.getElementById("deleteSubject").style.display = "inline-block";
        document.getElementById("updateSubject").style.display = "inline-block";
        document.getElementById("prepareForAdd").style.display = "inline-block";
        document.getElementById("prepareForAdd").style.visibility = "visible";
        document.getElementById("addSubject").style.visibility = "hidden";
        document.getElementById("addSubject").style.display = "none";
    }

    function setButtonsSave() {
        document.getElementById("deleteSubject").style.visibility = "hidden";
        document.getElementById("updateSubject").style.visibility = "hidden";
        document.getElementById("prepareForAdd").style.visibility = "hidden";
        document.getElementById("prepareForAdd").style.display = "none";
        document.getElementById("deleteSubject").style.display = "none";
        document.getElementById("updateSubject").style.display = "none";
        document.getElementById("saveUpdatedSubject").style.visibility = "hidden";
        document.getElementById("saveUpdatedSubject").style.display = "none";
        document.getElementById("addSubject").style.visibility = "visible";
        document.getElementById("addSubject").style.display = "inline-block"
        $('#subjectTitleInput').removeAttr('disabled');
        $('#subjectTitleInput').val('');
    }


    deleteSubjectButton.click(function (event) {
        var actionUrl = window.location.origin + deleteSubjectButton.attr('formaction');
        var subjectId = $('#subjectId').val();
        var subjectTitle = $('#subjectTitleInput').val();
        var formData = new FormData();
        formData.append("SubjectId", subjectId);
        formData.append("Title", subjectTitle);

        $.ajax({
            cache: false,
            contentType: false,
            processData: false,
            method: 'DELETE',
            type: 'DELETE',
            url: actionUrl,
            data: formData,
            success: function (data) {
                console.log('Subject' + data.subjectId);
                location.reload();
            }

        });
    });

    function setListAdd(data) {
        var list = document.getElementById("listOfSubjects");
        var li = document.createElement("li");
        var btn = document.createElement("BUTTON");
        btn.setAttribute('id', data.subjectId);
        btn.setAttribute('class', 'btn btn-primary subjectName');
        btn.setAttribute('style', 'border: none;  background-color: inherit; color:blue; display: inline-block; ');
        btn.setAttribute('name', data.title);
        btn.innerHTML = data.title;
        li.appendChild(btn);
        list.appendChild(li);
        $('body').on('click', function (e) {
            e.stopImmediatePropagation();
            e.preventDefault();
            addActionOnSubjectNameButton($('.subjectName'));
        });
        $('body').click();
    }
}
   function setButtonsStudent() {
        var addStudentButton = $('#addStudent');
        var tableStudents = $('#table_students');
        var updateStudent = $('#updateStudent');
        setTable();
        setActionOnButton(addStudentButton, 'POST', 'save');
        setActionOnButton(updateStudent, 'PUT', 'update');
        deleteRowFromTable('#table_students tbody', '#table_students', '.tableButtonDelete');

        function checkInputValuesForStudent() {
            if ($('#indexNumberStudent').val().trim() === '' || $('#inputStudentFirstName').val().trim() === ''
                || $('#inputStudentCity').val().trim() === '') {
                $('#errorStudent').html('<font style="color:red">Sva polja su obavezna!<font>');
                return false;
            }

            $('#errorStudent').html('');
            return true;
        }

        function setActionOnButton(button, method, action) {
            button.click(function (event) {
                if (!checkInputValuesForStudent()) {
                    return;
                }
                var actionUrl = window.location.origin + button.attr('formaction');
                var formData = new FormData();

                formData.append('StudentId', $('#studentId').val());
                formData.append('IndexNumber', $('#indexNumberStudent').val());
                formData.append('FirstName', $('#inputStudentFirstName').val());
                formData.append('LastName', $('#inputStudentLastName').val());
                formData.append('City', $('#inputStudentCity').val());
                document.getElementById('studentForm').reset();
                console.log(actionUrl);
                console.log(formData);

                $.ajax({
                    cache: false,
                    contentType: false,
                    processData: false,
                    method: method,
                    type: method,
                    url: actionUrl,
                    data: formData,
                    success: function (data) {
                        if (data != null) {
                            if (action === 'save') {
                                addRowToTable(data);
                                console.log(data);
                                setButtonsForSave();
                            } else {
                                location.reload();
                            }
                        } else {
                            alert('Doslo je do greske, pokusajte ponovo!');
                        }
                    }
                });

            });
        }

        function addRowToTable(data) {
            console.log('uslo');
            console.log('uslo2 ')
            var idData = data.studentId + ',' + data.indexNumber + ',' + data.firstName + ',' + data.lastName + ',' + data.city.split(" ").join(".");
            console.log(idData);
            tableStudents.DataTable().row.add([
                data.indexNumber,
                data.firstName,
                data.lastName,
                data.city,
                "<button type =\"button\" data-toggle=\"modal\" data-target=\examsModal\" asp-action=\"GetSubjectsAndMarksByIndexNumber\" asp-controller=\"Student\"  class = \"btn btn-primary tableButtonExams\" id=" + idData + " > Ispiti</button > ",
                "<button type =\"button\"  class = \"btn btn-success tableButtonUpdate\" id=" + idData + " > Izmeni</button > ",
                "<button type =\"button\" asp-action=\"Delete\" asp-controller=\"Student\"  class = \"btn btn-danger tableButtonDelete\" id=" + idData + "> Obriši</button > "
            ]).draw();

            reloadClickListeners();

        }

        function reloadClickListeners() {
            $('body').on('click', function (e) {
                e.stopImmediatePropagation();
                e.preventDefault();
                setClickListenerOnRowButtonUpdate();
                setClickListenerOnRowButtonDelete();
                setClickListenerOnButtonExams();
            });
            $('body').click();
        }

        function setTable() {
            tableStudents.DataTable();
            setClickListenerOnRowButtonDelete();
            setClickListenerOnRowButtonUpdate();
            setClickListenerOnButtonExams();
        }


        function setClickListenerOnRowButtonUpdate() {
            $('.tableButtonUpdate').click(function (event) {
                var data = event.currentTarget.id.split(',');
                $('#studentId').val(data[0]);
                $('#indexNumberStudent').val(data[1]);
                $('#inputStudentFirstName').val(data[2]);
                $('#inputStudentLastName').val(data[3]);
                $('#inputStudentCity').val(data[4].replace('.', ' '));
                setButtonsForUpdate();
            });
        }

        function setClickListenerOnRowButtonDelete() {
            $('.tableButtonDelete').click(function (event) {
                console.log('uslo u delete');
                var actionUrl = window.location.origin + $('.tableButtonDelete').attr('formaction');
                var formData = new FormData();
                var data = event.currentTarget.id.split(',');
                formData.append('StudentId', data[0]);
                formData.append('IndexNumber', data[1]);
                formData.append('FirstName', data[2]);
                formData.append('LastName', data[3]);
                formData.append('City', data[4]);
                document.getElementById("studentForm").reset();
                setButtonsForSave();
                $.ajax({
                    cache: false,
                    contentType: false,
                    processData: false,
                    method: 'DELETE',
                    type: 'DELETE',
                    url: actionUrl,
                    data: formData,
                    success: function (data) {
                    }

                });
            });
        }

        function setButtonsForUpdate() {
            document.getElementById("addStudent").style.visibility = "hidden";
            document.getElementById("addStudent").style.display = "none";
            document.getElementById("updateStudent").style.visibility = "visible";
            document.getElementById("updateStudent").style.display = "inline-block";

        }

        function setButtonsForSave() {
            document.getElementById("addStudent").style.visibility = "visible";
            document.getElementById("addStudent").style.display = "inline-block";
            document.getElementById("updateStudent").style.visibility = "hidden";
            document.getElementById("updateStudent").style.display = "none";

        }


        function setClickListenerOnButtonExams() {
            $('.tableButtonExams').click(function (event) {
                var actionUrl = window.location.origin + $('.tableButtonExams').attr('formaction');
                var data = event.currentTarget.id.split(',');
                console.log('index number');
                var formData = new FormData();
                formData.append("IndexNumber", data[1]);
                $.ajax({
                    cache: false,
                    contentType: false,
                    processData: false,
                    method: 'PUT',
                    type: 'PUT',
                    url: actionUrl,
                    data: formData,
                    success: function (data) {
                        console.log(data);
                        $('#examsModal').html(data);
                        $('#examsModal').modal();
                    }

                });


            });
        }


}
function deleteRowFromTable(tableBody, tableName, buttonName) {
    $(tableBody).on('click', buttonName, function () {
        $(tableName).DataTable()
            .row($(this).parents('tr'))
            .remove()
            .draw();
    });
}

$('#examPage').click(function (event) {
    window.location.href = "";
});

$('#subjectPage').click(function (event) {
    window.location.href = "/Subject/Subject";
});

$('#studentPage').click(function (event) {
    window.location.href = "/Student/Student";
});


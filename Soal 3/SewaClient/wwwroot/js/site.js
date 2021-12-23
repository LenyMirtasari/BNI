// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#table_id').DataTable({
        'ajax': {
            'url': "/Penyewas/LogHistory",
            'dataSrc': ''
        },
        'columns': [
            {
                "data": "no",
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "nama",
            },
            {
                "data": "noTelp",
            },
            {
                "data": "mulaiSewa",
                "render": function (date) {
                    var date;
                    date = date.toString();
                    dateTime = date.substring(0, 10);
                    return dateTime;
                }
            },
            {
                "data": "akhirSewa",
                "render": function (date) {
                    var date;
                    date = date.toString();
                    dateTime = date.substring(0, 10);
                    return dateTime;
                }
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {

                    CountDownTimer(`${row['akhirSewa']}`, 'coba1');
                    return `<div class="coba" ></div>
                            <div class="coba1" ></div>`
                }

            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal" onclick="ModalDetail(${row['logId']})" >
                              Detail
                            </button>
                            <button type="button" class="btn btn-success" onclick="Kembali(${row['logId']}, ${row['mobilId']})">Kembali</button>`
                }

            }
        ]
    });
});

function CountDownTimer(dt, id) {
    var end = new Date(dt);

    var _second = 1000;
    var _minute = _second * 60;
    var _hour = _minute * 60;
    var _day = _hour * 24;
    var timer;

    function showRemaining() {
        var now = new Date();
        var distance = end - now;
        if (distance < 0) {

            clearInterval(timer);
            document.getElementsByClassName(id).innerHTML = 'EXPIRED!';

            return;
        }
        var days = Math.floor(distance / _day);
        var hours = Math.floor((distance % _day) / _hour);
        var minutes = Math.floor((distance % _hour) / _minute);
        var seconds = Math.floor((distance % _minute) / _second);


        var ab = document.getElementsByClassName(id).innerHTML = days + 'days ' + hours + 'hrs' + minutes + 'mins ' + seconds + 'secs';
        $('.coba1').html(ab);
     /*   document.getElementById(id).innerHTML += hours + 'hrs ';
        document.getElementById(id).innerHTML += minutes + 'mins ';
        document.getElementById(id).innerHTML += seconds + 'secs';*/
    }

    timer = setInterval(showRemaining, 1000);
}


function ModalDetail(id) {
    console.log(id);
    listSW = "";
    $.ajax({
        url: "https://localhost:44307/API/Penyewas/LogHistory/"+id,
        success: function (result) {
            console.log(result);
            var mulaiSewa = result.mulaiSewa;
            mulaiSewaa = mulaiSewa.toString();
            mulaiSewa1 = mulaiSewaa.substring(0, 10);
            var akhirSewa = result.mulaiSewa;
            akhirSewaa = akhirSewa.toString();
            akhirSewa1 = akhirSewaa.substring(0, 10);
       
            listSW += `                                                         
	                    <div class="row form-group">                       
                            <label class="col-md-4" for="employeeId"><strong>Nama</strong></label>
                            <span class="col-md-7">: ${result.nama}</span>
                        </div>
                        <div class="row form-group">
                            <label class="col-md-4" for="employeeId"><strong>Alamat</strong></label>
                            <span class="col-md-7">: ${result.alamat}</span>
                        </div>
                        <div class="row form-group">
                            <label class="col-md-4" for="employeeId"><strong>No Telp</strong></label>
                            <span   class="col-md-7">: ${result.noTelp}</span>
                        </div>
                        <div class="row form-group">
                            <label class="col-md-4" for="employeeId"><strong>Jenis Mobil</strong></label>
                            <span class="col-md-7">: ${result.tipe}</span>
                        </div>
                        <div class="row form-group">
                            <label class="col-md-4" for="employeeId"><strong>Mulai Sewa</strong></label>
                            <span class="col-md-7">: ${mulaiSewa1}</span>
                        </div>
                        <div class="row form-group">
                            <label class="col-md-4" for="employeeId"><strong>Akhir Sewa</strong></label>
                            <span class="col-md-7">: ${akhirSewa1}</span>
                        </div>
                        ` ;

            $('#detail-sewa').html(listSW);
        }
    })
}

function Kembali(id, mobilId) {
    console.log(id, mobilId)
    var obj = new Object();
    obj.LogId = id;
    obj.MobilId = mobilId;
    console.log(obj);
    $.ajax({
        headers: {
            'Content-Type': 'application/json',
            'charset': 'utf-8'
        },
        url: "https://localhost:44307/API/Penyewas/Kembali",
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: 'json',
    }).then((result) => {
        console.log(result);
        if (result.status == 200) {
            Swal.fire(
                'Good job!',
                'Mobil Dikembalikan!',
                'success'
            )
          
            $('#table_id').DataTable().ajax.reload();
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Update Failed!',
                footer: 'All columns must be filled !'
            })
        }

    })
}

//Halaman Penyewa

$(document).ready(function () {
    $('#penyewa_id').DataTable({
        'ajax': {
            'url': "/Penyewas/GetAll",
            'dataSrc': ''
        },
        'columns': [
            {
                "data": "no",
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "nama"
            },
            {
                "data": "alamat"
            },
            {
                "data": "noTelp"
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return `
                            <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#SewaModal" onclick="ModalSewa(${row['penyewaId']})" >
                              Sewa
                            </button>
                            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#penyewaModal" onclick="ModalUpdate(${row['penyewaId']})" >
                              Update
                            </button>
                            <button type="button" class="btn btn-danger" onclick="Delete(${row['penyewaId']})">Delete</button>`
                }

            }
        ]
    })
});

//Register

$("#FormRegister").validate({
    rules: {
        Nama: {
            required: true
        },
        Alamat: {
            required: true
        },
        NoTelp: {
            required: true,
        }
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    },
    highlight: function (element) {
        $(element).closest('.form-control').addClass('is-invalid');
    },
    unhighlight: function (element) {
        $(element).closest('.form-control').removeClass('is-invalid');
    }
});

function Validation() {
    var a = $("#FormRegister").valid();
    console.log(a);
    if (a == true) {
        Insert();
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Register Failed!',
            footer: 'All columns must be filled !'

        })
    }
}

function clearTextBox() {
    $('#Nama').val("");
    $('#Alamat').val("");
    $('#NoTelp').val("");
    $('#Nama').css('border-color', 'lightgrey');
    $('#Alamat').css('border-color', 'lightgrey');
    $('#NoTelp').css('border-color', 'lightgrey');
}

function Insert() {
    var obj = new Object();
    obj.Nama = $("#Nama").val();
    obj.Alamat = $("#Alamat").val();
    obj.NoTelp = $("#NoTelp").val();
    console.log(obj);
    $.ajax({
        url: "/Penyewas/Register",
        type: "POST",
        data: obj 
    }).done((result) => {
        console.log(result);
        if (result == 200) {
            Swal.fire(
                'Good job!',
                'Data Inserted!',
                'success'
            )

            $("#registerModal").modal("toggle"),
            $('#registerModal').modal('hide'),
            clearTextBox(),
            $('#penyewa_id').DataTable().ajax.reload();

        } else if (result == 400) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Phone Number Already Existed !'
            })
        }

    }).fail((error) => {
        console.log(error)
        if (error == 400) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Phone Number Already Existed !'
            })
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Register Failed !'
            })
        }
    })
}

//Update Penyewa

function ModalUpdate(penyewaId) {
    console.log(penyewaId)
    $.ajax({
        url: "https://localhost:44307/API/Penyewas/" + penyewaId,
        success: function (result) {
            console.log(result);
            $("#PenyewaId1").val(result.penyewaId);
            $("#Nama1").val(result.nama);
            $("#Alamat1").val(result.alamat);
            $("#NoTelp1").val(result.noTelp);
        }
    })
}

function Update(id) {
    console.log(id);
    var obj = new Object();
    obj.PenyewaId = $("#PenyewaId1").val();
    obj.Nama = $("#Nama1").val();
    obj.Alamat = $("#Alamat1").val();
    obj.NoTelp = $("#NoTelp1").val();
    console.log(obj);
    $.ajax({
        headers: {
            'Content-Type': 'application/json',
            'charset': 'utf-8'
        },
        url: "https://localhost:44307/API/Penyewas/"+id,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: 'json',
    }).then((result) => {
        console.log(result);
        if (result.status == 200) {
            Swal.fire(
                'Good job!',
                'Data Updated!',
                'success'
            )
            $("#penyewaModal").modal("toggle"),
            $('#penyewaModal').modal('hide'),
            $('#penyewa_id').DataTable().ajax.reload();
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Update Failed!',
                footer: 'All columns must be filled !'
            })
        }

    })
}



$("#FormUpdate").validate({
    rules: {
        Nama: {
            required: true
        },
        Alamat: {
            required: true
        },
        NoTelp: {
            required: true,
        }
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    },
    highlight: function (element) {
        $(element).closest('.form-control').addClass('is-invalid');
    },
    unhighlight: function (element) {
        $(element).closest('.form-control').removeClass('is-invalid');
    }
});

function UpdateValidation(id) {
    var a = $("#FormUpdate").valid();
    console.log(a);
    if (a == true) {
        Update(id)
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Update Failed!',
            footer: 'All columns must be filled !'

        })
    }
}

function Delete(id) {
    console.log(id);
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        console.log(result);
        if (result.isConfirmed == true) {
            $.ajax({
                url: "https://localhost:44307/API/Penyewas/" + id,
                type: "Delete"
            }).then((result) => {
                console.log(result);
                if (result == 1) {
                    Swal.fire(
                        'Deleted!',
                        'Your data has been deleted.',
                        'success'
                    )
                    $('#penyewa_id').DataTable().ajax.reload();
                }
            })
        }
    })
}

//Sewa
$(document).ready(function () {
    $.ajax({
        url: "https://localhost:44307/API/Penyewas/Mobil",
        success: function (result) {
            console.log(result)
            var mobil = "";
            $.each(result, function (key, val) {
                mobil += ` <option value="${val.mobilId}">${val.tipe}</option>`;
            })

            $('#Mobil').html(mobil);
        }

    })


});


function ModalSewa(penyewaId) {
    console.log(penyewaId)
    $.ajax({
        url: "https://localhost:44307/API/Penyewas/" + penyewaId,
        success: function (result) {
            console.log(result);
            $("#PenyewaId2").val(result.penyewaId);
        }
    })
}

$("#FormSewa").validate({
    rules: {
        Mobil: {
            required: true
        },
        MulaiSewa: {
            required: true
        },
        AkhirSewa: {
            required: true,
            greaterThan: "#MulaiSewa"
        }
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    },
    highlight: function (element) {
        $(element).closest('.form-control').addClass('is-invalid');
    },
    unhighlight: function (element) {
        $(element).closest('.form-control').removeClass('is-invalid');
    }
});

$.validator.addMethod("greaterThan",
    function (value, element, params) {

        if (!/Invalid|NaN/.test(new Date(value))) {
            return new Date(value) > new Date($(params).val());
        }

        return isNaN(value) && isNaN($(params).val())
            || (Number(value) > Number($(params).val()));
    }, 'Tidak boleh kurang dari tanggal awal sewa');

function SewaValidation(id) {
    var a = $("#FormSewa").valid();
    console.log(a);
    if (a == true) {
        InsertSewa(id)
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Update Failed!',
            footer: 'All columns must be filled !'

        })
    }
}

function clearSewa() {
    $('#Mobil').val("");
    $('#MulaiSewa').val("");
    $('#AkhirSewa').val("");
    $('#Mobil').css('border-color', 'lightgrey');
    $('#MulaiSewa').css('border-color', 'lightgrey');
    $('#AkhirSewa').css('border-color', 'lightgrey');
}

function InsertSewa(id) {
    var obj = new Object();
    obj.PenyewaId = id;
    obj.MobilId = $("#Mobil").val();
    obj.MulaiSewa = $("#MulaiSewa").val();
    obj.AkhirSewa = $("#AkhirSewa").val();
    console.log(obj);
    $.ajax({
        headers: {
            'Content-Type': 'application/json',
            'charset': 'utf-8'
        },
        url: "https://localhost:44307/API/Penyewas/TambahSewa",
        type: "POST",
        data: JSON.stringify(obj)
    }).done((result) => {
        console.log(result);
        if (result == 0) {
            Swal.fire(
                'Good job!',
                'Data Inserted!',
                'success'
            )

            $("#SewaModal").modal("toggle"),
            $('#SewaModal').modal('hide'),
            clearSewa(),
                $('#table_id').DataTable().ajax.reload(),
                setTimeout(function () {

            window.location.href = "/Penyewas/Sewa";
                },1000)
        }

    }).fail((error) => {
        console.log(error)
        if (error.status == 404) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Date has passed !'
            })
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Register Failed !'
            })
        }
    })
}



const contadorDeProductosEnCarrito = document.getElementById('contadorDeProductosEnCarrito');
const estadoBotonProducto = document.getElementById('estadoBotonProducto');
const detalleTablaProductosAgregados = document.getElementById('detalleTablaProductosAgregados');
const carrito = [];

function AgregarProductosCarrito(producto) {
    GuardarEnCache(producto);
    MostrarContadorCarrito();
}
function GuardarEnCache(producto) {
    const { IdProducto, Nombre, Descripcion, Precio, Imagen, Estado } = producto;
    let NombreCategoria = producto.Categoria.Nombre;
    carrito.push({
        IdProducto,
        Nombre,
        Descripcion,
        Precio,
        Imagen,
        Estado,
        Categoria: {
            NombreCategoria
        }
    });
    localStorage.setItem('carritoCompras', JSON.stringify(carrito));
    localStorage.setItem('size', carrito.length);
}
function MostrarContadorCarrito() {
    contadorDeProductosEnCarrito.innerHTML = MostrarTamanioCarrito() === 0 ? 'Carrito' : `Carrito ( ${MostrarTamanioCarrito()} )`;
}
contadorDeProductosEnCarrito.innerHTML = MostrarTamanioCarrito() === null ? 'Carrito' : `Carrito ( ${MostrarTamanioCarrito()} )`;

function MostrarDatosEnCache() {
    return localStorage.getItem('carritoCompras');
}
function MostrarTamanioCarrito() {
    return localStorage.getItem('size');
}

//Eventos del DOM
contadorDeProductosEnCarrito.addEventListener('click', function (e) {
    window.location.href = "/Carrito/ListarCarrito";
});

//Mostramos los productos agregados al carrito
let jsonProductos = JSON.parse(MostrarDatosEnCache());
let _html = `
<thead>
    <tr>
        <th>#</th>
        <th>Nombre</th>
        <th>Categoria</th>
        <th>Imagen</th>
        <th>Precio</th>
        <th>Cantidad</th>
        <th>Total</th>
    </tr>
</thead>
`;
for (let i = 0; i < jsonProductos.length; i++) {
    _html += `
    <tr>
        <td style="width: 50px;">${i + 1}</td>
        <td style="width: 250px;">${jsonProductos[i].Nombre}</td>
        <td>${jsonProductos[i].Categoria.NombreCategoria}</td >
        <td style="width: 150px;"><img src="${jsonProductos[i].Imagen}" width='120' height='80' /></td>
        <td style="width: 50px;">${jsonProductos[i].Precio}</td>
        <td style="width: 50px;">1</td>
        <td style="width: 50px;">${jsonProductos[i].Precio * 1}</td>
    </tr>
    <br />
    `
        ;
}
_html += `
    <button class="btn btn-success btn-block">Guardar pedido</button>
`;
detalleTablaProductosAgregados.innerHTML = _html;
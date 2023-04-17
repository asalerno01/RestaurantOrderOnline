// import React, { useEffect, useState } from 'react';
// import { IoMdClose } from 'react-icons/io';
// import OrderItemAddons from './OrderItem_Addons';
// import OrderItemNoOptions from './OrderItem_NoOptions';
// import OrderItemGroups from './OrderItem_Groups';
// import ItemImage from '../../components/ItemImage';
// import OrderItemStyles from './css/OrderItem.module.css';
// import { isEmptyObject } from './functions/OrderFunctions';

// const EditItem = ({ itemI, setOrder, setEditItem, order, editItemIndex, setEditItemIndex }) => {
//     let item = itemI;
//     console.log(item)
//     console.log(JSON.stringify(order))
//     console.log(editItemIndex)
//     const [optionsSelected, setOptionsSelected] = useState({ groups: [], addons: [], noOptions: [] });
    
//     useEffect(() => {
//         if (editItemIndex !== null) {
//             item = order.orderItems[editItemIndex];
//             console.log(item)
//             setOptionsSelected({ groups: item.modifier.groups, addons: item.modifier.addons, noOptions: item.modifier.noOptions });
//         }
//     }, [editItemIndex]);

//     const handleEditItemClick = event => {
//         console.log("editing item")
//         console.log(item)
//         console.log(editItemIndex)
//         console.log(order.orderItems[editItemIndex])
//         const orderItem = {
//             "itemId": item.itemId,
//             "name": item.name,
//             "price": getPrice(),
//             "count": order.orderItems[editItemIndex].count,
//             "modifier": {
//                 "addons": optionsSelected.addons,
//                 "noOptions": optionsSelected.noOptions,
//                 "groups": optionsSelected.groups
//             }
//         }
//         console.log(orderItem.count)
//         const temp = Object.assign({}, order);
//         temp.orderItems[editItemIndex] = orderItem;
//         temp["subtotal"] = Number(temp["subtotal"]) + Number(orderItem['price'])
//         setOrder(temp);
//         setEditItem({});
//         setOptionsSelected({ groups: [], addons: [], noOptions: [] });
//         setEditItemIndex(null)

//     }

//     const handleClose = event => {
//         setEditItem({});
//         setOptionsSelected({ groups: [], addons: [], noOptions: [] });
//     }
//     if (isEmptyObject(item)) return <></>
//     else return (
//         <div className={OrderItemStyles.backdrop} onClick={handleClose}>
//             <div className={OrderItemStyles.container} onClick={e => e.stopPropagation()}>
//                 <div className={OrderItemStyles.header}>
//                     <button className={OrderItemStyles.close_button} onClick={handleClose}><IoMdClose size={"1.5em"}/></button>
//                 </div>
//                 <div className={OrderItemStyles.content}>
//                     <h1 className={OrderItemStyles.content_header}>{item.name}</h1>
//                     <span className={OrderItemStyles.description}>{item.description}</span>
//                     <div className={OrderItemStyles.image_wrapper}>
//                         <ItemImage itemName={item.name} />
//                     </div>
//                     <OrderItemGroups
//                         groups={item.modifier.groups}
//                         optionsSelected={optionsSelected}
//                         setOptionsSelected={setOptionsSelected}
//                     />
//                     <OrderItemAddons 
//                         itemName={item.name}
//                         addons={item.modifier.addons}
//                         optionsSelected={optionsSelected}
//                         setOptionsSelected={setOptionsSelected}
//                     />
//                     <OrderItemNoOptions 
//                         itemName={item.name}
//                         noOptions={item.modifier.noOptions}
//                         optionsSelected={optionsSelected}
//                         setOptionsSelected={setOptionsSelected}
//                     />
//                 </div>
//                 <div className={OrderItemStyles.footer}>
//                     <button className={OrderItemStyles.add_button} onClick={handleEditItemClick}>Update item - ${getPrice()}</button>
//                 </div>
//             </div>
//         </div>
//     )
// }

// export default EditItem;
import './AddFrend.css';
import React from 'react';

import Avatar from '../Avatar/Avatar';
const AddFrend = props => { 

const frendList = props.res.map( frend => <div><Avatar size="small" /><p>{frend.name}</p></div>)

    let frendClasses = 'add-frend-con';
    if (props.show) {
        frendClasses = 'add-frend-con open';
    }
    return (
    <div className="add-frend">
        <p onClick={props.click}> + </p>
        <div className={frendClasses}>
        <input onChange={props.change} type="text"></input>
            <div className="frend-search hiden">
                {frendList}
            </div>
        </div>
    </div>
    );

}


export default AddFrend;
import React, { Component } from 'react';

import './SideDrawer.css';
import ItemList from '../ItemList/ItemList';

import AddFrend from '../AddFrend/AddFrend';
import Add from '../Add/Add';
import Avatar from '../Avatar/Avatar';

class SideDrawer extends Component {
  
  state = {
    result : [],
    addFrend: false,
    addServer: false,
  };

  FrendToggleClickHandler = () => {
    this.setState((prevState) => {
      return {addFrend: !prevState.addFrend};
    });
  };
  ServerToggleClickHandler = () => {
    this.setState((prevState) => {
      return {addServer: !prevState.addServer};
    });
  };

  FrendSearchHandler = (event) => {
    console.log(event.target.value)
  };
  serverAddBtn = () => {
    console.log(this.newServerName);
  }
  serverNameH = (event) => {
    this.newServerName = event.target.value;
  }
  render() {
    
    let drawerClasses = 'side-drawer';
    if (this.props.show) {
      drawerClasses = 'side-drawer open';
    }
  
  return (
    <div className={drawerClasses}>
        <Avatar link={this.props.user.userPic} size="big"/>
        <p>{this.props.user.name}</p>
          <ItemList clickHandler={this.props.click} drag={this.props.drag} avatar={true} list={this.props.frends}  name="Frends"  expand={this.props.fToggle} show={this.props.toggleF}>
          <AddFrend click={this.FrendToggleClickHandler} show={this.state.addFrend} res={this.state.result} change={this.FrendSearchHandler}></AddFrend>
          </ItemList>
          <ItemList clickHandler={this.props.sClick} avatar={false} list={this.props.servers} name="Servers" expand={this.props.sToggle} show={this.props.toggleS}>
            <Add show={this.state.addServer} click={this.ServerToggleClickHandler} add={this.serverAddBtn} change={this.serverNameH}></Add>
          </ItemList>
 
    </div>
  );
  }
};

export default SideDrawer;

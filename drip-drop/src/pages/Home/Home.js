import React, { Component } from 'react';
// import './Home.css'

import Toolbar from '../../components/Toolbar/Toolbar';
import SideDrawer from '../../components/SideDrawer/SideDrawer';
import Backdrop from '../../components/Backdrop/Backdrop';
import ChatBox from '../../components/Chat/ChatBox/ChatBox';
import ItemList from '../../components/ItemList/ItemList';
import Add from '../../components/Add/Add';

class Home extends Component {
  state = {
    sideDrawerOpen: false,
    frendListOpen: true,
    serversListOpen: false,
    addChatsListOpen: false,
    frendList : [   
    {name: "miske"},
    {name: "ivan"},
    {name: "JOca"}
  ],
  serversList: [
    {name : "fagets"},
    {name : "jeb se"}
  ],
  chatU : {
    id : "ziva",
    userPic : "./logo.svg"
  },
  user : {
    name : "KrisKum",
    userPic : "https://scontent.fbeg1-1.fna.fbcdn.net/v/t1.0-9/p720x720/31948539_2124710084223154_6396547342857666560_o.jpg?_nc_cat=106&_nc_ohc=I_DuxEMVp0MAX9QmUL6&_nc_ht=scontent.fbeg1-1.fna&oh=0de96b254eb68c2f1a73312b14f05038&oe=5EAF12F0",
  },
  chat : [
    {from : "miske", message: "jeb se", id: 1},
    {from : "ziva", message: "i ti", id: 2},
  ],
  server: { chats: [{name : "muda"}]},
  result: [],
 };

  componentDidMount() {

    this.props.user.frends.forEach( frend => {

        fetch(`http://localhost:32345/api/User/${frend.id}`)
        .then(res => res.json())
        .then(frend => this.setState(prevState => { return { frendList : [...prevState.frendList , 
          { name: frend.username , avatar: frend.avatar, nick: frend.nickname , id: frend.useruid}]} }));
    });

    this.props.user.servers.forEach( server => {

        fetch(`http://localhost:32345/api/Server/${server.id}`)
        .then(res => res.json())
        .then(server => this.setState(prevState => { return { serversList : [...prevState.serversList, 
          {...server , id: server.serveruid}]} }));
    });
  }
  frendsToggleOpen = () => {
    this.setState((prevState) => {
      return {frendListOpen: !prevState.frendListOpen};
    });
  };
  chatsToggleOpen = () => {
    this.setState((prevState) => {
      return {addChatsListOpen: !prevState.addChatsListOpen};
    });
  };

  serversToggleOpen = () => {
    this.setState((prevState) => {
      return {serversListOpen: !prevState.serversListOpen};
    });
  };

  drawerToggleClickHandler = () => {
    this.setState((prevState) => {
      return {sideDrawerOpen: !prevState.sideDrawerOpen};
    });
  };

  backdropClickHandler = () => {
    this.setState({sideDrawerOpen: false});
  };

  itemClick = (user) => {
    fetch(`http://localhost:32345/api/Chat/${user.id}`)
    .then(res => res.json())
    .then(newChat => this.setState(prevState => { return { chat : newChat} }));
  };

  serverClick = (serverr) => {
     this.setState(prevState => { return { server : serverr} });
  };
  
  chatClick = (chat) => {
    fetch(`http://localhost:32345/api/Chat/${chat.id}`)
    .then(res => res.json())
    .then(newChat => this.setState(prevState => { return { chat : newChat} }));
  };
  

  render() {
    let backdrop;

    if (this.state.sideDrawerOpen) {
      backdrop = <Backdrop click={this.backdropClickHandler} />
    }
    return (
      <div style={{height: '100%'}}>
        <Toolbar  drawerClickHandler={this.drawerToggleClickHandler} />
        <SideDrawer drag={() => console.log('oprem')} click={this.itemClick} sClick={this.serverClick} fToggle={this.frendsToggleOpen} sToggle={this.serversToggleOpen} toggleF={this.state.frendListOpen}
        toggleS={this.state.serversListOpen} frends={this.state.frendList} servers={this.state.serversList}
        show={this.state.sideDrawerOpen} user={this.state.user}  />
        {backdrop}
        <main style={{marginTop: '56px' }}>
        <div style={{flexGrow : "1" }}>
          <div style={ this.state.server.chats.length === 0 ? {display: 'none'} : {minWidth: '235px', marginTop: "35px"} }>
            <ItemList  clickHandler={this.chatClick} avatar={false} list={this.state.server.chats}  name="Chats"  expand={() => 0} show={true}>
              <Add show={this.state.addChatsListOpen} click={this.chatsToggleOpen}/>
            </ItemList>
          </div>
        </div>
        <ChatBox user={this.state.user} chatUser={this.state.chatU} chat={this.state.chat} ></ChatBox>
        <div style={{flexGrow : "1"}}></div>
        </main>
        
      </div>
    );
  }
}

export default Home;

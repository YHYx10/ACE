<template>
  <div class="lobbies__item"
       @click="toLobbie(lobbie.id)"
       :style="{ 'background-image': `url(${background})` }">
    <div class="layer"></div>
    <div v-show="!lobbie.started" class="layer-hover"></div>
    <div class="item__header">
      <div class="header__places">
        <img src="/img/arenaMenu/user.svg" class="header__icon" alt="user">
        <span class="places__current">{{ currentPlayers }}</span>
        <span class="places__separator">/</span>
        <span class="places__maximum">{{ lobbie.maxPlayers }}</span>
      </div>
    </div>
    <div class="header__desc">
      <span class="header__status">{{loc(lobbie.started ? 'arena:lobby:status:1' : 'arena:lobby:status:2')}}</span>
      <span class="header__title">{{ lobbie.title }}</span>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters, mapMutations } from 'vuex'

export default {
  data() {
    return {
      active: false,
    }
  },
        
  name: 'LobbiesItem',

  props: ['lobbie'],
        
  computed: {
    ...mapState('arenaMenu', ['backgrounds']),

    ...mapGetters('localization', ['loc']),

    currentPlayers: function () {
      return this.lobbie.redTeam.length + this.lobbie.greenTeam.length
    },

    background: function () {
      const rightItem = this.backgrounds.find(item => item.title === this.lobbie.title)
      return rightItem.image
    }
  },

  methods: {
    ...mapMutations('arenaMenu', [
      'setCurrentLobbieId',
      'setIsCreate'
    ]),
    
    toLobbie: function (id) {
      // this.setCurrentLobbieId(id)
      // this.setIsCreate(false)
      // if (this.lobbie.started) {
      //   // some method
      // } else {
      //   //window.mp.trigger('ARENA::OPEN::LOBBY::SERVER', id)
      // }
      if(this.lobbie.started || this.lobbie.isMapChange) return
      this.setCurrentLobbieId(id)
      this.setIsCreate(false)
    }
  }
}
</script>

<style lang="scss" scoped>
.lobbies__item {
  width: 10rem;
  height: 18rem;
  display: flex;
  flex-direction: column;
  background-size: cover;
  background-position: center;
  padding: 1rem;
  position: relative;
  overflow: hidden;
  font-family: Akrobat;
  &:hover {
    .layer-hover {
      box-shadow: inset 0 -0.2rem 0 #301934 ;
      opacity: 1;
      transition: 0.5 ease-in;
    }
  }
  .layer-hover {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    transition: 0.2s ease-out;
    box-shadow: inset 0 -0.2rem 0 transparent;
    opacity: 0;
    background: linear-gradient(360deg, rgba(255, 0, 11, 0.33) 0%, rgba(255, 0, 11, 0) 50%);
  }
  .layer {
    background: rgba(1, 1, 1, 0.2);
    box-shadow: inset 0rem 0rem 6.5rem #000000;
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
  }
  .header__title {
    text-transform: uppercase;
    font-weight: bold;
    margin-top: auto;
    margin-bottom: auto;
    font-size: 1.6rem;
  }
  .header__desc {
    //margin-top: 8.25rem;
    position: absolute;
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    text-shadow: 0 .2rem .6rem rgba(0, 0, 0, 0.25);
    color: #FFF;
    bottom: 1rem;
  }
  .header__status {
    margin-top: auto;
    margin-bottom: auto;
    text-transform: uppercase;
    font-weight: 700;
    font-size: 1rem;
    line-height: 1.2rem;
  }
  .item__header {
    width: 100%;
    display: flex;
    color: #fff;
    font-weight: 700;
    font-size: 1.8rem;
    line-height: 2.15rem;
    justify-content: flex-end;
    align-items: center;
    z-index: 1;
    .header__places {
      display: flex;
      align-items: center;
      gap: 0.25rem;
      margin-right: auto;
      font-weight: 700;
      font-size: 1rem;
      line-height: 1.2rem;
    }
    .header__icon {
      width: 1.4rem;
      height: 1.4rem;
    }
  }
  .item__btn {
    background: #B6D300;
    box-shadow: 0rem 1rem 4.5rem rgba(168, 195, 2, 0.5);
    border-radius: 5rem;
    width: 11rem;
    position: relative;
    height: 3rem;
    .btn__text {
      color: #FFF;
     text-transform: uppercase;
      font-size: 1rem;
      letter-spacing: 0.03em;
      font-weight: bold;
      margin-right: .6rem;
    }
    .btn_img {
      margin: auto;
      position: absolute;
      top: 0; bottom: 0;
      height: .9rem;
    }
    &:hover {
      box-shadow: 0 0 0 0;
    }
    &--watch {
      background-image: url('../../../../../public/img/arenaMenu/btn-bg-black.jpg');
      color: #fff;
  }
  }
}
</style>

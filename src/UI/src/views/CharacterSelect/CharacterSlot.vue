<template>
  <div v-if="index < slotsCount" class="select_person__item select_person__item-disable">
    <div class="select_person__logosm-one">
      <img src="/img/characterSelect/logo-sm.png" alt="">
    </div>
    <div class="select_person__logolg">
      <img src="/img/characterSelect/logo-lg.png" alt="">
    </div>
    <div class="select_person__logosm-two">
      <img src="/img/characterSelect/logo-sm.png" alt="">
    </div>
    <div class="select_person__slot">
      <p></p>
      <h3>New character</h3>
      <h4>Would you like to try it out in an alternative role?Then create a new story about a new character</h4>
    </div>
    <div class="select_person__button">
      <button 
        class="btn btn--disable"
        @click="createCharacter"
        >
        Create
      </button>
    </div>
  </div>
  <div v-else class="select_person__item select_person__item-disable">
    <div class="select_person__logosm-one">
      <img src="/img/characterSelect/logo-sm.png" alt="">
    </div>
    <div class="select_person__logolg">
      <img src="/img/characterSelect/logo-lg.png" alt="">
    </div>
    <div class="select_person__logosm-two">
      <img src="/img/characterSelect/logo-sm.png" alt="">
    </div>
    <div class="select_person__slot">
      <p></p>
      <h3>The slot is not available </h3>
      <h4>To unlock an additional slot, buy it in a shop</h4>
    </div>
    <div class="select_person__button">
      <button 
        class="btn btn--disable"
        @click="setModal(true, 0, index)"
        >
   Unlock
      </button>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
export default {
  name: 'CharacterSlot',

  props: {
    index: Number,
    item: Object,
    setModal: Function,

  },
  data() {
    return {
      lastClick: 0,
      floodTime: 1500
    }
  },
  computed: {
    ...mapState('characterSelect', ['slotsCount']),
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    createCharacter: function() {
      if(this.lastClick > Date.now()) return;
      this.lastClick = Date.now() + this.floodTime;
      window.mp.triggerServer('auth:char:select', this.index)
    }
  }
}
</script>

<style lang="scss" scoped>

body {
  margin: 0;
  font-family: 'Akrobat';
  overflow-anchor: none;
  background-image: url('/img/characterSelect/bg-home.jpg');
  background-size: cover;
  background-repeat: no-repeat;
  color: #fff;
  height: 100vh;
}
.select_person {
  padding-top: 2.964240102171138vh;

  &__item {
    position: relative;
    transition: all .3s ease .1s;

    &:hover {
      cursor: pointer;
      margin-bottom: 1.5vh;
      margin-top: -1.5vh;
      //transform: scale(1.025);
      border: 0.2554278416347382vh solid #301934 ;
    }


    &-disable {
      background: rgba(255, 255, 255, 0.01);
      border: 0.2554278416347382vh solid rgba(255, 255, 255, 0.09);
      border-radius: 1.1494252873563218vh;
      background-image: url('/img/characterSelect/bg-disable-item.png');
      background-repeat: no-repeat;
      background-size: cover;
    }
  }

  &__slot {
    display: flex;
    align-items: center;
    flex-direction: column;
    justify-content: center;
    height: 65.671775vh;

    p {
      font-weight: 600;
      font-size: 1.9157088122605364vh;
      line-height: 104%;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.25);
      margin-top: 34%;
    }

    h3 {
      font-weight: 800;
      font-size: 3.0651340996168583vh;
      line-height: 3.1928480204342273vh;
      text-transform: uppercase;
      background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      text-fill-color: transparent;
      text-shadow: 0 0 14.303959131545339vh rgba(255, 255, 255, 0.25);
      margin-bottom: 0.2554278416347382vh;
    }

    h4 {
      font-weight: 600;
      font-size: 1.6602809706257982vh;
      line-height: 104%;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.25);
      max-width: 24.393358876117496vh;
      text-align: center;
    }
  }
  &__logosm-one {
    position: absolute;
    top: 5.363984674329502vh;
    left: 50%;
    transform: translateX(-50%);

    img {
      width: 15.070242656449553vh;
      height: 15.32567049808429vh;
    }
  }

  &__logosm-two {
    position: absolute;
    bottom: 4.736275565123789vh;
    left: 50%;
    transform: translateX(-50%);

    img {
      width: 15.070242656449553vh;
      height: 15.32567049808429vh;
    }
  }

  &__logolg {
    position: absolute;
    top: 21.32822477650064vh;
    left: 50%;
    transform: translateX(-50%);

    img {
      width: 39.84674329501916vh;
      height: 40.229885057471265vh;
      transform: rotate(90deg);
    }
  }
}

.btn {
  cursor: pointer;
  border: unset;

  &--disable {
    font-family: 'Akrobat';
    background: linear-gradient(180deg, rgba(71, 44, 132, 0.25) 0%, rgba(75, 0, 130, 0.25) 100%);
    border: 0.1277139208173691vh solid #301934 ;
    box-shadow: inset 0 0 1.9157088122605364vh rgba(75, 0, 130, 0.86);
    font-weight: 600;
    font-size: 1.7879948914431674vh;
    line-height: 2.1711366538952745vh;
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.25);
    padding: 3.1928480204342273vh 0;
    width: 85%;
    margin-top: 6.4vh;
    margin-right: 3vh;
    margin-left: 3vh;
    position: relative;
    z-index: 999999999999999;
    transition: all .3s ease .1s;

    &:hover {
      color: #fff;
      background: linear-gradient(180deg, #921117 0%, #591b87 100%);
    }
  }
}
</style>

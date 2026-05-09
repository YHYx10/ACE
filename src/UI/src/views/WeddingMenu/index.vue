<template>
  <div class="wedding-menu">
    <div class="flowers-top" />
    <div class="flowers-bottom" />
    <ExitCross @click="exit" class="exit-cross"  />
    <div class="cross" />
    <div class="row">
      <div class="form">
        <div class="title">marriage </div>
        <div class="input-block">
          <div class="subtitle">The name of the player you want to make an offer</div>
          <input v-model="name" type="text" placeholder="Enter the name">
        </div>
        <div class="input-block">
          <div class="subtitle">The surname of the player to whom you want to make an offer</div>
          <input v-model="surename" type="text" placeholder="Enter your last name...">
        </div>
        <DefaultBtn @click="propose">offer</DefaultBtn>
      </div>
      <img src="img/weddingMenu/couple.png" alt="">
    </div>
  </div>
</template>

<script>
import ExitCross from '../UI/components/ExitCross'
import DefaultBtn from '../UI/button/DefaultBtn'
import { mapState, mapGetters } from 'vuex'

export default {
  name: 'WeddingMenu', 

  data: function () {
    return {
      name: '',
      surename: ''
    }
  },

  computed: {
    ...mapState('weddingMenu', [
      'isWeddingComplete',
      'congratulationsName',
      'disableExit'
    ]),
    ...mapGetters('localization',['loc'])
  },

  components: {
    ExitCross,
    DefaultBtn
  },

  methods: {
    propose: function () {
      if (!this.name.length || !this.surename.length) {
        return
      }

      const player = this.name.concat('_', this.surename)

      this.name = ''
      this.surename = ''
      
      window.mp.trigger('marriage:inputName', player)
    },

    exit: function () {
      window.mp.trigger('marriage:cancelPropose')
    }
  }
}
</script>

<style lang="scss" scoped>
div,
button,
input {
  font-family: Akrobat;
  font-weight: 700;
  color: #FFFFFF;
}
.wedding-menu {
  width: 82.315vh;
  height: 43.704vh;
  background: rgba(0, 0, 0, 0.95);
  position: relative;
  overflow: hidden;
  display: flex;
  align-items: center;
  justify-content: center;

  .flowers-top,
  .flowers-bottom {
    position: absolute;
    width: 28.241vh;
    height: 13.241vh;
    background-image: url('/img/weddingMenu/flowers.png');
    background-size: cover;
    background-repeat: no-repeat;
  }
  .flowers-top {
    left: -5.556vh;
    top: -7.963vh;
  }
  .flowers-bottom {
    left: 61.944vh;
    top: 37.5vh;
  }
  .exit-cross {
    position: absolute;
    top: 1.852vh;
    right: 1.852vh;
  }
  .row {
    display: flex;
    align-items: center;
    justify-content: center;
    .form {
      .title {
        font-size: 3.333vh;
        line-height: 4.167vh;
        margin-bottom: 2.593vh;
        text-transform: uppercase;
      }
      .input-block {
        display: flex;
        flex-direction: column;
        gap: 0.37vh;
        .subtitle {
          font-size: 1.111vh;
          line-height: 1.389vh;
          margin: 0.926vh 0;
          text-transform: uppercase;
        }
        input {
          width: 28.704vh;
          height: 6.019vh;
          display: flex;
          justify-content: center;
          padding-left: 1.852vh;
          border: none;
          outline: none;
          background: rgba(217, 217, 217, 0.05);
          font-size: 1.296vh;
          &::placeholder {
            color: rgba(255, 255,255, 0.5);
            text-transform: uppercase;
            font-size: 1.296vh;
          }
        }
      }
      button {
        margin-top: 1.852vh;
        width: 28.704vh;
        height: 6.944vh;
        font-size: 2.222vh;
        font-weight: 600;
        text-transform: uppercase;
        color: #FFFFFF;
      }
    }

    img {
      margin-left: 8.889vh;
      width: 28.148vh;
      height: 31.296vh;
    }
  }
}
</style>

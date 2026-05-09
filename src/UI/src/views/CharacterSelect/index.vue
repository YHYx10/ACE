<template>
<body>
  <main class="main">
    <div class="select_person">
      <div class="select_person__container container">
        <div class="select_person__block">
          <div class="select_person__left">
            <h1 class="select_person__title">
        The choice of character
            </h1>
            <p class="select_person__descr">
           You can have up to 3 characters in one account
            </p>
          </div>
          <div class="select_person_right">
            <div class="select_person__info">
              <div class="select_person__text">
                <h3>Astro Coins</h3>
                <h4>{{coin}}</h4>
              </div>
              <div class="select_person__dollars">
                $
              </div>
            </div>
          </div>
        </div>

        <div class="select_person__items">
          
        <Component 
                v-for="(item, index) in slots"
                :key="index" 
                :is="item == null ? 'CharacterSlot' : 'Character'" 
                :index="index" 
                :item="item"
                :setModal="setModal"
          />
          <Modal 
            v-if="modal.show"
            :modal="modal"
            :setModal="setModal"
          />

        </div>
      </div>
    </div>
  </main>
</body>

</template>

<script>
import Character from './Character'
import CharacterSlot from './CharacterSlot'
import Modal from './Modal'

import {mapGetters, mapState} from 'vuex'

export default {
  name: 'CharacterSelect',

  components:{
    Character,
    CharacterSlot,
    Modal
  },

  data: function() {
    return {
      modal: {
        show: false,
        type: null,
        index: 0
      }
    }
  },

  computed:{
    ...mapState('characterSelect', ['slots', 'coin']),   
    ...mapGetters('localization', ['loc']),
    // charactersSlots: function() {
    //   return this.slots.filter(element => element !== null)
    // },
    // emptySlots: function() {
    //   return this.slots.filter(element => element === null)
    // }
  },

  methods: {
    setModal: function(show, type, index) {
      this.modal.type = type
      this.modal.index = index;
      this.modal.show = show;
    },
    buyCoins: function() {
      window.mp.trigger('characterSelect:buyCoins')
    },
    exitMenu: function() {
      window.mp.trigger('characterSelect:exitMenu')
    },
    keyUp: function(key) {
      if (key.keyCode === 27) { 
        this.exitMenu()
      }
    }
  },

  mounted() {
    window.addEventListener('keyup', this.keyUp)
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
  width: 100vw;
  height: 100vh;
}

.container {
  max-width: 167.56066411238825vh;
  margin: 0 auto;
  padding: 0 1.9157088122605364vh;
}

.select_person {
  padding-top: 2.964240102171138vh;

  &__block {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 1.469987228607918vh;
  }

  &__title {
    font-weight: 800;
    font-size: 6.130268199233717vh;
    line-height: 7.407407407407407vh;
    background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
    text-fill-color: transparent;
    text-shadow: 0 0 14.303959131545339vh rgba(255, 255, 255, 0.25);
  }

  &__descr {
    color: #fff;
    font-weight: 600;
    font-size: 1.9157088122605364vh;
    line-height: 2.2988505747126435vh;
    text-transform: uppercase;
  }

  &__info {
    display: flex;
    align-items: center;
  }

  &__text {

    h3 {
      font-weight: 600;
      font-size: 1.9157088122605364vh;
      line-height: 104%;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.25);
    }

    h4 {
      color: #fff;
      font-weight: 800;
      font-size: 3.0651340996168583vh;
      line-height: 3.7037037037037037vh;
    }
  }

  &__dollars {
    color: rgba(0, 0, 0, 0.33);
    width: 5.236270753512133vh;
    height: 5.236270753512133vh;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 100%;
    margin-left: 1.5325670498084292vh;
    font-weight: 800;
    font-size: 2.554278416347382vh;
    line-height: 3.0651340996168583vh;
    background: linear-gradient(180deg, #5CFF80 0%, #35AC4F 100%);
    box-shadow: 0 0 1.0472541507024264vh rgba(92, 255, 128, 0.25);
  }

  &__items {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-gap: 6.768837803320562vh;
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

</style>

<template>
  <div class="tattoo-shop">
    <div class="left-col">
      <div class="header">
        <div class="title">
          TATTOO
          <span>Salon</span>
        </div>
        <div class="subtitle">
     Removing tattoos
        </div>
      </div>
      <div class="main">
        <div
          class="list"
        >
            <TattooItem
              v-for="(tattoo, index) in tattooList"
              :key="index"
              :title="tattoo.name"
              :img="tattoo.img"
              :selected="current === tattoo"
              @click="selectTattoo(tattoo, index)"
            />
        </div>
      </div>
      <div class="price-block" v-if="current">
        <div class="price">
          <span class="title">
        The price of information
          </span>
          <div class="value">
            ${{ current.price.toLocaleString("ru") }}
          </div>
        </div>
        <button @click="removeTattoo">
      Make a tattoo
        </button>
      </div>
    </div>

    <div class="tattoo-shop_right">
      <button class="close" @click="close">
        <img src="/img/autoSchool/question/x.svg" alt="" />
      </button>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import TattooItem from './TattooItem.vue'

export default {
  name: 'RemovingTattoo',
  components: {
    TattooItem
},
  computed: {
    ...mapState('tattooShop', ['tattoos', 'currentTattoos', 'gender']),
    ...mapGetters('localization', ['loc']),
    ...mapGetters('tattooShop', ['cameraPosition']),
    tattooList() {
      const tattooList = []
      for (const key in this.tattoos) {
        this.tattoos[key].forEach((tattoo) => {
          const index = this.currentTattoos.findIndex(
            (t) =>
              t.Collection === tattoo.Dict &&
              (t.Overlay === tattoo.Male || t.Overlay === tattoo.Female)
          )
          if (index !== -1) {
            tattooList.push({
              name: tattoo.Name,
              img: `img/tattooShop/tattoo/${tattoo.Name.toLowerCase().replace(
                /\s/g,
                '_'
              )}.png`,
              price: tattoo.Price,
              desc: 'desc',
              slot: tattoo.Slots[0],
              index,
            })
          }
        })
      }
      return tattooList
    },
  },
  data() {
    return {
      spamProtection: 0,
      current: 0,
    }
  },
  methods: {
    selectTattoo(item) {
      this.current = item
      var camera = this.cameraPosition(item.slot)
      window.mp.trigger('camMoveCamZ', camera.cameraZ)
      window.mp.trigger('camMovePointZ', camera.pointZ)
      window.mp.trigger('camMoveAngleX', camera.angle)
    },
    removeTattoo() {
      if (this.current.index === undefined || this.spamProtection > Date.now())
        return
      this.spamProtection = Date.now() + 1000
      window.mp.triggerServer('tattoo:remove:buy', this.current.index)
    },
    close() {
      window.mp.trigger('tattoo:remove:close')
    }
  },
}
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.tattoo-shop {
  width: 100vw;
  height: 100vh;
  position: relative;
  display: flex;
  justify-content: space-between;
  align-items: flex-start;

  &::after {
    content: '';
    position: absolute;
    z-index: -1;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: radial-gradient(
      50% 94.42% at 50% 50%,
      rgba(0, 0, 0, 0) 0%,
      #000000 100%
    );
    opacity: 0.8;
  }

  .left-col {
    width: auto;
    display: flex;
    flex-direction: column;
    margin-left: 2.315vh;
  }

  .header {
    margin-bottom: 2.315vh;
    .title {
      display: flex;
      align-items: center;
      font-weight: 800;
      font-size: 5.093vh;
      line-height: 6.111vh;
      color: #ffffff;
      span {
        display: block;
        background: linear-gradient(
          180deg,
          #301934  0%,
          #ea0505 49.48%,
          #301934  100%
        );
        -webkit-background-clip: text;
        -webkit-text-fill-color: transparent;
        background-clip: text;
        text-fill-color: transparent;
        text-shadow: 0vh 0vh conv(49) rgba(255, 0, 0, 0.52);
      }
    }
    .subtitle {
      margin-top: 0.093vh;
      font-weight: 700;
      font-size: 2.963vh;
      line-height: 3.519vh;
      color: #ffffff;
    }
  }
  .main {
    overflow-y: scroll;
    overflow-x: hidden;
    direction: rtl;
    padding-left: 0.926vh;
    height: 65.463vh;
    &::-webkit-scrollbar {
      width: 0.463vh;
      &-thumb {
        background: #301934 ;
      }
      &-track {
        background: rgba(0, 0, 0, 0.25);
      }
    }
    .list {
      direction: ltr;
      display: grid;
      gap: 0.926vh;
      grid-template-columns: repeat(2, 1fr);
      
    }
  }
  
  

 


  &_right {
    button {
      width: conv(50);
      height: conv(50);
      display: flex;
      align-items: center;
      justify-content: center;
      cursor: pointer;
      background: none;
      border: none;
      outline: none;

      img {
        height: conv(30);
      }
    }
  }

  .price-block {
    margin-top: 1.944vh;
    .price {
      display: flex;
      align-items: center;
      justify-content: center;
      gap: 1.111vh;
      font-weight: 700;
      .title {
        font-size: 1.481vh;
        line-height: 1.759vh;
        color: #ffffff;
      }
      .value {
        font-size: 2.963vh;
        line-height: 3.519vh;
        text-transform: uppercase;
        color: #A0FF98;
      }
    }
    button {
      margin-top: 1.759vh;
      width: 32.5vh;
      height: 6.944vh;
      text-transform: uppercase;
      color: #ffffff;
      font-weight: 700;
      font-size: 2.222vh;
      line-height: 2.685vh;
      border: 0.093vmin solid rgba($color: #301934 , $alpha: 1);
      background: linear-gradient(
        rgba($color: #301934 , $alpha: 0.25),
        rgba($color: #591b87, $alpha: 0.25)
      );
      transition: 0.3s ease;
      box-shadow: inset 0 0 1.389vmin rgba($color: #301934 , $alpha: 0.86);

      &:hover {
        transition: 0.3s ease;
        box-shadow: inset 0vh 0vh 13.889vh #301934 ;
        filter: drop-shadow(0vh 0vh conv(15) rgba(71, 44, 132, 0.5));
      }
    }
  }
}
</style>
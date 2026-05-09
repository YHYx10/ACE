<template>
  <div class="modal">
    <close-button :styles="{ position: 'absolute', top: '0', right: '0' }" @close="$emit('close')"/>
    <div class="modal__side">
      <div class="heading">
        <div class="category">{{ typeName }}</div>
        <div class="title">
          <div class="title__name">
            <div class="text" v-if="!isEdit">{{ property.business.name }}</div>
            <textarea
                v-else
                v-model="property.business.name"
                class="area"
                maxlength="20"
                v-focus
            ></textarea>
          </div>
          <button class="change-btn" @click="changeName"><img
              :src="`img/optionsMenu/statisticsTab/businessModal/${isEdit ? 'check' : 'pencil'}.svg`"></button>
        </div>
      </div>
      <div class="main">
        <div class="main__item">
          <div class="category">{{ loc('mmain:stats:biz:taxph') }}</div>
          <div class="title">{{ property.business.tax }}$</div>
          <div class="subtitle">Short description for example</div>
        </div>
        <div class="main__item">
          <div class="category">{{ loc('mmain:stats:biz:taxcount') }}</div>
          <div class="title">{{ property.business.taxCount + '/' + taxMax }}$</div>
          <div class="subtitle">Short description for example</div>
        </div>
        <div class="main__item">
          <div class="category">{{ loc('mmain:stats:biz:cost') }}</div>
          <div class="title">{{ property.business.price }}$</div>
          <div class="subtitle"> A brief description for example </div>
        </div>
        <div class="item__btn" @click="sell">{{ loc('mmain:stats:biz:cell') }}</div>
        <div class="item__btn" style="margin-top: 0.5rem;"  @click="sellToPlayer">{{ loc('mmain:stats:biz:pcell') }}</div>
      </div>
    </div>
    <div class="modal__main">
      <div class="heading">
        <div class="category">section</div>
        <div class="title">{{ loc('mmain:stats:biz:prod') }}</div>
      </div>
      <div class="products">
        <products-item
            v-for="(item, index) in property.business.products"
            :key="index"
            :item="item"
            @onChange="changePrice"
        />
      </div>
    </div>
  </div>
</template>

<script>
import {mapState, mapGetters, mapMutations} from 'vuex'
import CloseButton from '../../../common/CloseButton.vue'
import ProductsItem from './common/ProductsItem'

export default {
  name: 'BusinessModal',

  components: {
    ProductsItem,
    CloseButton
  },

  data: function () {
    return {
      isEdit: false,
      title: '',
      bizNames: [
        "24/7",
        "Petrol Station",
        "Premium Autoroom",
        "Luxor Autoroom",
        "Low Autoroom",
        "Motoroom",
        "Gun",
        "Clothes Shop",
        "Burger-Shot",
        "Tatto",
        "Barbershop",
        "Masks shop",
        "LS Customs",
        "Car wash",
        "Pet shop",
        "Super CarRoom",
        "Autorepair",
        "Rent car",
        "Car trader",
        "Casino",
        "Retro Autoroom",
        "JDM Autoroom",
        "New Autoroom",
        "New Autoroom",
        "New Autoroom",
        "New Autoroom",
        "New Autoroom",
        "Furniture",
        "Illegal Shop"
      ]
    }
  },

  computed: {
    ...mapState('optionsMenu', ['property', 'statistics']),
    ...mapGetters('localization', ['loc']),
    taxMax() {
      const days = this.statistics.premium ? 30 : 7;
      return this.property.business.price / 100 * 0.013 * 1.9 * 24 * days
      //return this.property.business.tax * days * 24;
    },
    typeName() {
      return this.bizNames[this.property.business.type] || 'noInfo'
    }
  },

  methods: {
    ...mapMutations('optionsMenu', ['setDialog']),
    setTitle: function (value) {
      this.title = value
    },
    sell() {
      window.mp.triggerServer("mmenu:biz:sell", this.property.business.number);
      this.$emit("close");
    },
    sellToPlayer() {
      this.setDialog({
        input: 'number',
        input2: 'number',
        callback: (val, val2) => {
          val = parseInt(val);
          val2 = parseInt(val2);
          if (isNaN(val) || isNaN(val2) || val2 < this.property.business.price / 2) {
            window.setData('notifyList/notify', {
              type: 1,
              position: 2,
              message: "mmain:frac:dialog:data:wrong",
              time: 3000
            });
          } else {
            window.mp.triggerServer('mmenu:biz:sell:player', val, val2);
          }
        },
        value: '',
        placeholder: 'mmain:stats:biz:pcell:pl',
        placeholder2: 'mmain:stats:biz:pcell:pl2',
        tittle: `mmain:stats:biz:pcell:tit`,
        subtittle: 'mmain:stats:biz:pcell:sub',
        bg: undefined
      });
    },
    changePrice(name, price) {
      window.mp.triggerServer("mmenu:product:price:set", this.property.business.number, name, price);
    },
    changeName() {
      if (this.isEdit) {
        window.mp.triggerServer("mmenu:biz:name:set", this.property.business.number, this.property.business.name);
      }
      this.isEdit = !this.isEdit;
    }
  },

  directives: {
    focus: {
      inserted: function (el) {
        el.focus()
      }
    }
  },

  mounted: function () {
    this.setTitle(this.property.business.title)
  }
}
</script>

<style lang="scss" scoped>
.modal {
  position: absolute;
  display: flex;
  top: -0.1rem;
  left: 0;
  width: 100%;
  height: 100%;
  background: url("/img/optionsMenu/bg.png"), rgba(0, 0, 0, 0.96);
  background-blend-mode: overlay;

  &__side {
    width: 18rem;
    margin: 0 2rem 0 0.7rem;

    .heading {
      margin-bottom: 1rem;
      .category {
        font-size: 0.9rem;
      }

      .title {
        display: flex;
        align-items: start;
        justify-content: space-between;

        &__name {
          word-break: break-word;
          width: 16rem;

          & .area {
            width: 100%;
            height: 100%;
            font-family: inherit;
            font-size: inherit;
            line-height: inherit;
            font-weight: inherit;
            text-transform: inherit;
            letter-spacing: inherit;
            background: inherit;
            border: inherit;
            color: inherit;
            overflow: hidden;
            resize: none;
            outline: none;
          }
        }

        .change-btn {
          background: none;
          margin: 0.6rem 0 0 0.9rem;
          width: 1.1rem;
          height: 1.1rem;
          opacity: 0.5;
          transition: 0.3s ease;

          & img {
            width: 100%;
            height: 100%;
            object-fit: contain;
          }

          &:hover {
            opacity: 1;
          }
        }
      }
    }

    .main {
      &__item {
        height: 10rem;
        margin-bottom: 0.5rem;
        border: 1px solid rgba(255, 255, 255, 0.1);
        padding: 1rem 1.25rem 1.4rem 1.75rem;
        background: bottom right / cover no-repeat url("/img/optionsMenu/statisticsTab/businessModal/bg.png");

        .category {
          font-size: 0.8rem;
          line-height: 0.9rem;
          letter-spacing: 0.05em;
        }

        .title {
          font-size: 1.7rem;
          line-height: 1.9rem;
          color: #5CFF80;
        }
      }
    }
  }

  &__main {
    flex: 1 1 100%;
    .heading {
      margin: 0 0 1rem 0.7rem;
    }
    .products {
      height: 39.3rem;
      overflow-y: scroll;

      scrollbar-width: thin;
      scrollbar-color: #5cff80 #444444;

      &::-webkit-scrollbar {
        display: block;
        width: 0.1rem;
        height: 0;
      }

      &::-webkit-scrollbar-track {
        background: #444444;
      }

      &::-webkit-scrollbar-thumb {
        background-color: #5cff80;
      }
    }
  }
}
</style>

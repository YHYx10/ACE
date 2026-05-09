<template>
  <div class="products__item billet-item">
    <div class="products__item-title">{{ item.title }}</div>
    <div class="products__item-amount">
      <progress-circle
          :styles="{ margin: '0 0.75rem 0 0' }"
          :maxVal="item.maxCount"
          :curVal="item.curCount"
      />
      <div class="products__item-amount-text">
        <div class="key">{{ loc('mmain:stats:biz:prod:count') }}</div>
        <div class="value">{{ item.curCount + '/' + item.maxCount }}</div>
      </div>
    </div>
    <div class="products__item-price">
      <div class="key">{{ loc('mmain:stats:biz:prod:price') }}</div>
      <div class="value">
        <div v-if="!isEdit" class="value-text">
          <div class="value-text__val">{{ item.price.toLocaleString() }}</div>
          <span v-if="businessTypesWithPercent.includes(bizType)">{{item.price.length > 15 ? '... %' : '%'}}</span>
          <span v-else>{{item.price.length > 15 ? '... $' : '$'}}</span>
        </div>
        <input
            v-else
            type="text"
            class="value-input"
            v-model="price"
            @keypress="isNumber($event)"
            v-focus
        >
      </div>
    </div>
    <div class="products__item-change-btn item__btn" @click="changePrice">{{isEdit ? 'confirm' : 'change'}}</div>
  </div>
</template>

<script>
import ProgressCircle from './ProgressCircle'
import {mapGetters} from 'vuex'

export default {
  name: 'ProductsItem',

  components: {
    ProgressCircle
  },

  computed: {
    ...mapGetters('localization', ['loc'])
  },
  props: {
    item: Object,
    /** Business.type */
    bizType: Number
  },

  data: function () {
    return {
      isEdit: false,
      price: this.item.price,
      businessTypesWithPercent: [2, 3, 4, 11, 12, 7, 5, 9, 10, 27]
    }
  },

  methods: {
    isNumber: function(evt) {
      evt = (evt) ? evt : window.event;
      let charCode = (evt.which) ? evt.which : evt.keyCode;
      if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode !== 46) {
        evt.preventDefault();
      } else {
        return true;
      }
    },
    setPrice: function (value) {
      this.price = value
    },
    changePrice() {
      if (this.isEdit) {
        console.log('changePrice', this.price);
        const sum = parseInt(this.price)
        if (!isNaN(sum))
          this.$emit("onChange", this.item.title, sum);
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
    // this.setPrice(this.item.price)
  }
}
</script>

<style lang="scss" scoped>
.products__item {
  display: flex;
  align-items: center;
  padding: 0.7rem 2rem 0.7rem 2.5rem !important;
  transition: 0.2s ease;

  &:hover:before {
    left: 0;
    height: 100%;
  }

  .key {
    font-weight: 600;
    color: rgba(255,255,255,0.3);
    font-size: 0.8rem;
  }

  .value {
    font-weight: 600;
    font-size: 0.9rem;
    letter-spacing: 0.03em;
    color: #00FF38;
  }

  &-title {
    font-size: 1.2rem;
    font-weight: 600;
    line-height: 1.2rem;
    color: #FFFFFF;
    width: 10rem;
    margin: 0 2rem 0 0;
  }

  &-amount {
    display: flex;
    align-items: center;
  }

  &-price {
    .value {
      &-text {
        width: 7.2rem;
        display: flex;
        &__val {
          max-width: 14ch;
          overflow: hidden;
        }
      }
      &-input {
        height: 100%;
        width: 7.2rem;
        overflow: hidden;
        font-family: inherit;
        font-size: inherit;
        line-height: inherit;
        font-weight: inherit;
        text-transform: inherit;
        letter-spacing: inherit;
        background: inherit;
        border: inherit;
        color: inherit;
        resize: none;
        outline: none;
      }
    }
  }

  &-change-btn {
    width: 8.5rem;
  }
}
</style>

<template>
  <div class="card-item">
    <div class="title">{{ item.unicName }}</div>
    <div class="row">
      <div class="price">$ {{item.cost}}</div>
      <div class="btn btn-remove" @click="$emit('decrease', index)">
        <svg xmlns="http://www.w3.org/2000/svg" width="8" height="3" fill="none" viewBox="0 0 8 3">
          <path fill="#fff" d="M.003.18v1.86h7V.18h-7Z"/>
        </svg>
      </div>
      <div v-if="input" class="input">
        <input :value="item.count" type="number" @input="event => parseNumber(event.target.value)" />
      </div>
      <div v-else class="value">
        {{item.count}}
      </div>
      <div class="btn btn-add" @click="$emit('increase', index)">
        <svg xmlns="http://www.w3.org/2000/svg" width="9" height="9" fill="none" viewBox="0 0 9 9">
          <path fill="#fff" d="M8.23 3.9H5.39V.98h-1.8V3.9H.75v1.8h2.84v2.92h1.8V5.7h2.84V3.9Z"/>
        </svg>
      </div>
      <div class="btn btn-remove-all" @click="$emit('deleteItem', index)">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="12"
          height="12"
          fill="none"
          viewBox="0 0 12 12"
        >
          <path
            fill="#fff"
            d="M10.791 0 6 4.791 1.209 0 0 1.209 4.791 6 0 10.791 1.209 12 6 7.209 10.791 12 12 10.791 7.209 6 12 1.209 10.791 0Z"
          />
        </svg>
      </div>
    </div>
  </div>
</template>

<script>

export default {
  name: 'ShoppingCartItem',

  props: {
    item: Object,
    index: Number,
    input: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    parseNumber(value){
      let number = Math.abs(Number(value)) || 1
      this.$emit('updateValue', number)
    }
  }
}
</script>

<style lang="scss" scoped>
div,
span {
  color: #ffffff;
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  text-transform: uppercase;
}

.card-item {
  font-family: 'Akrobat';
  padding-left: 1.852vh;
  flex-shrink: 0;
  display: flex;
  flex-direction: column;
  width: 27.315vh;
  height: 8.704vh;
  background: rgba(255, 255, 255, 0.02);
  .title {
    font-weight: 700;
    font-size: 1.852vh;
    line-height: 2.315vh;
    text-transform: uppercase;
    margin-top: 1.389vh;
    color: #ffffff;
  }
  .row {
    display: flex;
    align-items: center;
    margin-top: 0.741vh;
    .price {
      display: flex;
      justify-content: left;
      width: 11.296vh;
      margin-right: 2.578vh;
      font-size: 2.222vh;
      line-height: 2.778vh;
      color: #a0ff98;
    }
    .btn-add,
    .btn-remove {
      display: flex;
      justify-content: center;
      align-items: center;
      width: 2.13vh;
      height: 2.13vh;
      background: rgba(255, 255, 255, 0.1);
    }
    .btn-remove {
      svg {
        margin-top: 0.185vh;
      }
    }

    .value {
      display: flex;
      justify-content: center;
      align-items: center;
      width: 2.87vh;
      font-size: 1.481vh;
      line-height: 1.852vh;
    }

    .input {
      display: flex;
      align-items: center;
      justify-content: center;
      height: 100%;
        input {
          background: transparent;
          outline: 1px solid rgba(255, 255, 255, 0.1);
          text-align: center;
          width: 5vh;
          height: 2.13vh;
          font-size: 1.2vh;
          color: white;
          margin: 0 0.2vh;
          &::-webkit-outer-spin-button,
          &::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
          }
      }
    }

    .btn-remove-all {
      margin-left: 1.667vh;
    }
  }
}
</style>

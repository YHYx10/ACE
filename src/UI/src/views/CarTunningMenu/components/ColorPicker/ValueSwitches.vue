<template>
  <div class="value-switches">
    <div class="wrap">
      <button @click="slide(index - 1)">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="32"
          height="32"
          fill="none"
          viewBox="0 0 32 32"
        >
          <path
            fill="#fff"
            d="M22 23.975 19.981 26 10 16l9.981-10L22 8.025 14.044 16 22 23.975Z"
          />
        </svg>
      </button>
      <div>
        <div class="title">
          {{ title }}
        </div>
        <div class="value">
          {{ list[index] }}
        </div>
      </div>
      <button @click="slide(index + 1)" style="transform: translate(1px);">
        <svg
          style="transform: scale(-1)"
          xmlns="http://www.w3.org/2000/svg"
          width="32"
          height="32"
          fill="none"
          viewBox="0 0 32 32"
        >
          <path
            fill="#fff"
            d="M22 23.975 19.981 26 10 16l9.981-10L22 8.025 14.044 16 22 23.975Z"
          />
        </svg>
      </button>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'ValueSelector',

  props: {
    title: String,
    /** type: string[] */
    list: Array,
    index: Number,
  },

  data: function() {
    return {
      value: this.index,
    }
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    slide(newIndex) {
      if (newIndex < 0) this.value = this.list.length - 1
      else if (this.list.length - 1 < newIndex) this.value = 0
      else this.value = newIndex
      this.$emit('input', this.value)
    },
  },
}
</script>

<style lang="scss" scoped>
.value-switches {
  width: 100%;
  height: 50px;
  display: flex;
  align-items: center;
  margin: 0;
  padding: 0;
  background: rgba(7, 7, 7, 0.7);

  .wrap {
    margin: 0;
    padding: 0;
    display: flex;
    width: 100%;
    position: relative;
    justify-content: space-between;
    align-items: center;

    div {
      text-align: center;
      font-family: 'Akrobat';
      font-style: normal;
      color: #ffffff;
      display: flex;
      flex-direction: column;
      justify-content: center;
      text-transform: uppercase;
      font-weight: 700;
      .title {
        font-size: 14px;
        line-height: 17px;
        color: #ffffff;
      }
      .value {
        font-size: 12px;
        line-height: 15px;
        color: rgba(255, 255, 255, 0.5);
      }
    }

    button {
      width: 51px;
      height: 50px;
      display: flex;
      justify-content: center;
      align-items: center;
      background: linear-gradient(180deg, #301934  0%, #591b87 100%);
      svg {
        width: 32px;
        stroke: #fff;
      }
      &:hover {
        background: linear-gradient(180deg, #301934  -50%, #591b87 100%);
       
      }
     
    }
  }
}
</style>

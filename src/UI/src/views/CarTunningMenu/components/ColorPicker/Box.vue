<template>
  <div class="color-type">
    <span class="color-type__name">
      {{ name }}
    </span>
    <input
      v-model="modelColor"
      class="color-type__value"
    >
  </div>
</template>

<script>
export default {
  props: {
    name: {
      type: String,
      default: ''
    },
    color: {
      type: String,
      default: ''
    }
  },

  computed: {
    modelColor: {
      get: function() {
        return this.color
      },
      set: function(val) {
        this.$emit('inputColor', val)
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.color-type {
  display: flex;
  height: 35px;
  width: 100%;
  background: rgba(32, 32, 32, 0.95);

  div, input {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 16px;
    line-height: 20px;
    color: #fff;
  }
  
  margin-bottom: 5px;
  &__name {
    width: 51px;
    color: #fff;
    display: flex;
    align-items: center;
    justify-content: center;
    background: rgba(0, 0, 0, 0.4);
  }
  &__value {
    width: 163px;
    text-align: center;
    border: 0;
    font-size: 20px;
    line-height: 25px;
     display: flex;
    align-items: center;
    justify-content: center;
    background: transparent;
  }
}
</style>

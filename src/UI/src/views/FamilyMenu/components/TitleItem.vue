<template>
  <div class="title-item">
    <div class="divide" />
    <div v-show="name" class="name">{{name}}</div>
    <div class="values" :style="{fontSize: toVH(fontSize), lineHeight: toVH(fontSize)}">
      <div v-for="item in value" :key="item" class="value">
        {{item}}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    name: String,
    /**
     * type: string[]
     */
    value: Array,
    fontSize: Number
  },
  methods: {
    toVH(px){
      return (px / 1080) * 100 + 'vh'
    }
  }
}
</script>

<style lang="scss" scoped>

.title-item {
  display: flex;
  flex-direction: column;
  padding-left: 1.944vh;
  position: relative;
  gap: 0.463vh;
  .divide {
    position: absolute;
    width: 0.185vh;
    height: 2.407vh;
    background: rgba(255, 255, 255, 1);
    box-shadow: 0vh 0vh 1.296vh rgba(255, 255, 255, 0.55);
    top: 0;
    left: 0;
  }
  .name {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 1.111vh;
    line-height: 1.111vh;
    text-transform: uppercase;

    color: rgba(255, 255, 255, 0.55);
  }
  .values {
    display: flex;
    flex-direction: column;
    font-size: 4.444vh;
    .value {
      font-family: 'Akrobat';
      font-style: normal;
      font-weight: 700;
      text-transform: uppercase;

      color: #FFFFFF;
    }
  }
}
</style>
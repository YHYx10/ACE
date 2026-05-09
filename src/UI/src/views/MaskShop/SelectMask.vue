<template>
  <div class="select-mask">
    <ValueSwitches
      class="value-switches"
      :list="list.slice(0, getMaxPages)"
      :index="page"
      v-model="page"
      isShortStyle
    >
      <div class="value">
        <div class="highlight">{{page + 1}}</div>
        <div>/ {{getMaxPages}}</div>
      </div>
    </ValueSwitches>
    <div @wheel="onWheel" class="list">
      <!-- {{ list }} -->
      <!-- {{page}} -->
      <div
        class="wrap"
        :style="{
          transform: `translate(-${page/8*100}%)`,
        }"
      >
        <Card
          v-for="(item, key) of list"
          :key="key"
          :price="item.Price"
          :imgSize="0.65"
          :img="`/img/maskShop/${item.Variation}.png`"
          :title="`Mask ${item.Variation}`"
          :isActive="key === value"
          @onSelect="() => $emit('input', key)"
        />
      </div>
    </div>
  </div>
</template>
<script>
import Card from '../UI/components/Card.vue'
import ValueSwitches from '../UI/components/ValueSwitches.vue'
export default {
  props: {
    value: Number,
    list: {
      type: Array,
      default() {
        return []
      },
    },
  },
  computed: {
    getMaxPages() {
      return Math.ceil(this.list.length)
    },
  },
  data() {
    return {
      page: 0,
    }
  },
  components: { Card, ValueSwitches },
  methods: {
    onWheel(e) {
      e.deltaY < 0 ? this.prevPage() : this.nextPage()
    },
    nextPage() {
      if(this.page + 1 < this.getMaxPages) this.page += 1
    },
    prevPage() {
      if(this.page > 0) this.page -= 1
    },
  },
}
</script>

<style lang="scss" scoped>
.select-mask {
  display: flex;
  align-items: flex-end;
  overflow: hidden;
  flex-direction: column;
  .value-switches {
    padding: 0%;
    width: 18.148vh;
    height: auto;
    margin-right:  1.852vh;
    margin-bottom: 2.5vh;
    box-shadow: none;
    &::before {
      background: transparent;
    }
    .value {
      width: auto;
      display: flex;
      flex-direction: row;
      justify-content: center;
      align-items: flex-end;
      font-family: 'Montserrat';
      font-style: normal;
      font-weight: 600;
      font-size: 1.667vh;
      line-height: 1.667vh;
      .highlight {
        font-size: 2.222vh;
        color: #ffffff;
      }
      div {
        color: rgba(255, 255, 255, 0.35);
      }
    }
  }
  .list {
    color: #ffffff;
    display: flex;
    gap: 1.852vh;
    overflow: hidden;
    margin-left: -0.926vh;
    .wrap {
      display: flex;
      gap: 1.855vh;
      margin-left: 0.926vh;
      width: 135.54vh;
      transition: 0.4s;
    }
    .card {
      transition: 0.6s;
      flex: none;
      width: 15.093vh;
      height: 15.185vh;
    }
  }
}
</style>

<template>
  <div class="about-us-page">
    
    <div class="info">
      <div class="title">{{loc('PortOrders_17')}}</div>
      <div class="text">{{loc('PortOrders_18')}}<br/><br/>{{loc('PortOrders_22')}}<br/><br/>{{loc('PortOrders_23')}}</div>
      <div class="line"/>
      <div class="stats">
        <div class="item">
          <div class="name">{{loc('PortOrders_19')}}</div>
          <div class="value">25 000+ </div>
        </div>
        <div class="item">
          <div class="name">{{loc('PortOrders_20')}}</div>
          <div class="value">15</div>
        </div>
        <div class="item">
          <div class="name">{{loc('PortOrders_21')}}</div>
          <div class="value">29</div>
        </div>
      </div>
    </div>
    <img src="/img/portOrders/preview.png" alt="">
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'About',

  computed: {
    ...mapGetters('localization', ['loc'])
  }
}
</script>

<style lang="scss" scoped>
.about-us-page{
  margin-top: 6.667vh;
  width: 100%;
  color: #fff;
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  text-transform: uppercase;
  .info {
    display: flex;
    flex-direction: column;
  }
  .title {
    font-weight: 700;
    font-size: 4.444vh;
    line-height: 5.37vh;
    margin-bottom: 3.148vh;
  }
  .text {
    font-weight: 700;
    font-size: 2.222vh;
    line-height: 2.685vh;
    max-width: 44.444vh;
  }
  .line {
    margin: 4.537vh 0 5.278vh 2.593vh;
    width: 6.944vh;
    height: 0.185vh;
    background: #ffffff;
    border: 0.097vh solid rgba(255, 255, 255, 0.09);
    box-shadow: 0vh 0vh 1.355vh rgba(255, 255, 255, 0.55);
   
  }
  .stats {
    width: 34.259vh;
    display: flex;
    flex-wrap: wrap;
    gap: 3.333vh 4.63vh;
  }
  .item {

    .name {
      font-weight: 700;
      font-size: 2.222vh;
      line-height: 2.685vh;
      color: rgba(255, 255, 255, 0.5);
    }
    .value {
      color: #FFCC47;
      font-weight: 700;
      font-size: 3.333vh;
      line-height: 3.981vh;
    }
  }

  img {
    width: 80.648vh;
    height: 58.009vh;
  }
}
</style>

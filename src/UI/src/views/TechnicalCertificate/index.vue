<template>
  <div class="technical-certificate">
    <div v-if="false" class="car-info">
      <div class="car-info__desc">{{loc('TechnicalCertificate_1')}}</div>
      <div class="car-info__name">
        <div class="desc">{{loc('TechnicalCertificate_2')}}</div>
        <div class="value">{{carState.carName}}</div>
      </div>
      <div class="car-info__state">
        <div class="car-info__state-condition">
          <div class="desc">{{loc('TechnicalCertificate_3')}}</div>
          <div class="value">{{carState.generalState}}%</div>
        </div>
        <div class="car-info__state-run">
          <div class="desc">{{loc('TechnicalCertificate_4')}}</div>
          <div class="value">{{carState.miliage}} {{loc('TechnicalCertificate_7')}}</div>
        </div>
      </div>
      <div class="car-info__owner">
        <div class="desc">{{loc('TechnicalCertificate_5')}}</div>
        <div class="value">{{carState.owner}}</div>
      </div>
      <div class="car-info__price">
        <div class="desc">{{loc('TechnicalCertificate_6')}}</div>
        <div class="value">${{carState.price}}</div>
      </div>
    </div>
    <div class="container">
      <div class="header">
        <h1> Technical passport</h1>
        <h1>{{carState.carName}}</h1>
      </div>
      <div class="car-info">
        <div class="item">
          <img src="../../../public/img/technicalCertificate/key.svg" alt="">
          <div class="col">
            <div class="title">
             Car condition
            </div>
            <div class="value">
              {{carState.generalState}}%
            </div>
          </div>
        </div>
        <div class="item">
          <img src="../../../public/img/technicalCertificate/timer.svg" alt="">
          <div class="col">
            <div class="title">
              Car mileage
            </div>
            <div class="value">
              {{carState.miliage}} km
            </div>
          </div>
        </div>
        <div class="item">
          <img src="../../../public/img/technicalCertificate/wallet.svg" alt="">
          <div class="col">
            <div class="title">
            price
            </div>
            <div class="value">
              ${{carState.price.toLocaleString('en-US').replaceAll(',',' ')}}
            </div>
          </div>
        </div>
        <div class="item">
          <img src="../../../public/img/technicalCertificate/man.svg" alt="">
          <div class="col">
            <div class="title">
             owner
            </div>
            <div class="value">
              {{carState.owner}}
            </div>
          </div>
        </div>
      </div>
      <div class="car-state">
        <list-item
          v-for="item in carState.mechState"
          :key="item.id"
          :item="item"
        />
      </div>
    </div>
    
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import ListItem from './ListItem'

export default {
  name: 'TechnicalCertificate',

  components: {
    ListItem
  },

  computed: {
    ...mapState('technicalCertificate', ['carState']),
    ...mapGetters('localization', ['loc'])
  }
}
</script>

<style lang="scss" scoped>
.technical-certificate{
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  background: rgba(21, 19, 19, 0.85);
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  text-transform: uppercase;
  font-family: Akrobat;
  .container {
    width: 1216px;
  }
  .header {
    display: flex;
    justify-content: space-between;
    margin-bottom: 10px;
    color: #fff;
    text-transform: uppercase;
  }
  .car-info {
    background-image: url(../../../public/img/technicalCertificate/bg.png);
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 63px;
    .item {
      display: flex;
      align-items: center;
      gap: 20px;
      height: 131px;
      .col {
        display: flex;
        flex-direction: column;
        .title {
          font-weight: 700;
          font-size: 12px;
          line-height: 14px;
          text-transform: uppercase;
          color: rgba(255, 255, 255, 0.55);
        }
        .value {
          font-weight: 700;
          font-size: 32px;
          line-height: 38px;
          text-transform: uppercase;
          color: #FFFFFF;
          &.green {
            font-weight: 900;
            font-size: 32px;
            line-height: 38px;
            color: #A0FF98;
          }
        }
      }
    }
  }
  // .car-info{
  //   display: flex;
  //   flex-flow: column;
  //   align-items: flex-start;
  //   justify-content: space-between;
  //   background:  rgba(2, 3, 16, 0.9) url('/img/technicalCertificate/bg.png') no-repeat center;
  //   background-size: cover;
  //   width: 27.5rem;
  //   min-width: 27.5rem;
  //   height: 25rem;
  //   padding: 3.5rem 2.75rem;
  //   position: relative;
  //   letter-spacing: 0.03em;
  //   &__desc{
  //     position: absolute;
  //     font-size: 2.3rem;
  //     line-height: 2.75rem;
  //     color: rgba(255, 255, 255, 0.2);
  //     transform: rotate(-90deg) translateX(2rem);
  //     transform-origin: 100% 100%;
  //     top: 0;
  //     right: 0;
  //   }
  //   &__name{
  //     display: flex;
  //     flex-flow: column;
  //     align-items: flex-start;
  //     .desc{
  //       font-size: .9rem;
  //       line-height: 1.1rem;
  //       color: rgba(255, 255, 255, 0.5);
  //     }
  //     .value{
  //       font-size: 2.3rem;
  //       line-height: 2.75rem;
  //       font-weight: 300;
  //       color: #FFFFFF;
  //     }
  //   }
  //   &__state{
  //     display: flex;
  //     align-items: center;
  //     justify-content: flex-start;
  //     width: 100%;
  //     position: relative;
  //     padding-bottom: 2rem;
  //     margin-bottom: 2rem;
  //     &:after{
  //       content: '';
  //       width: 100%;
  //       height: .1rem;
  //       background: #B6D300;
  //       box-shadow: 0 .5rem 2.5rem rgba(168, 195, 2, 0.6);
  //       border-radius: 1px;
  //       position: absolute;
  //       bottom: 0;
  //       left: 0;
  //     }
  //     &-condition{
  //       display: flex;
  //       align-items: center;
  //       margin-right: 2rem;
  //       .desc{
  //         font-size: .9rem;
  //         line-height: 1.2rem;
  //         color: rgba(255, 255, 255, 0.5);
  //         max-width: 4rem;
  //         margin-right: .35rem;
  //       }
  //       .value{
  //         font-weight: 300;
  //         font-size: 2.5rem;
  //         line-height: 3rem;
  //         text-align: center;
  //         color: #B6D300;
  //         text-shadow: 0px 10px 50px rgba(168, 195, 2, 0.6);
  //       }
  //     }
  //     &-run{
  //       display: flex;
  //       flex-flow: column;
  //       .desc{
  //         font-size: .9rem;
  //         line-height: 1.1rem;
  //         color: rgba(255, 255, 255, 0.5);
  //       }
  //       .value{
  //         font-weight: 300;
  //         font-size: 1.3rem;
  //         line-height: 1.55rem;
  //         color: #FFFFFF;
  //       }
  //     }
  //   }
  //   &__owner, &__price{
  //     display: flex;
  //     flex-flow: column;
  //     .desc{
  //       font-size: 1rem;
  //       line-height: 1.2rem;
  //       color: rgba(255, 255, 255, 0.5);
  //     }
  //     .value{
  //       font-size: 1.5rem;
  //       line-height: 1.8rem;
  //       text-transform: uppercase;
  //       color: #FFFFFF;
  //       font-weight: 300;
  //     }
  //   }
  //   &__price{
  //     margin-top: 1.25rem;
  //   }
  // }
  .car-state{
    margin-top: 30px;
    display: flex;
    justify-content: space-between;
  }
}
</style>
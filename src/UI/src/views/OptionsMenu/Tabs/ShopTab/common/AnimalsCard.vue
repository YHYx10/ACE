<template>
    <div class="animals-card">
        <div class="background">
            <svg class="background-1" xmlns="http://www.w3.org/2000/svg"  viewBox="0 0 408 386"
                fill="none">
                <path d="M453.702 -260L-192.298 386H101.603L453.702 33.9009L805.801 386H1099.7L453.702 -260Z"
                    fill="url(#paint0_linear_1_32)" fill-opacity="0.02" />
                <!-- <defs>
                    <linearGradient id="paint0_linear_1_32" x1="453.702" y1="-260" x2="453.702" y2="386"
                        gradientUnits="userSpaceOnUse">
                        <stop stop-color="white" />
                        <stop offset="1" stop-color="white" stop-opacity="0" />
                    </linearGradient>
                </defs> -->
            </svg>
            <svg class="background-2" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 408 349"
                fill="none">
                <path d="M453.702 0L-192.298 640H101.603L453.702 291.171L805.801 640H1099.7L453.702 0Z"
                    fill="url(#paint0_linear_1_31)" fill-opacity="0.02" />
                <!-- <defs>
                    <linearGradient id="paint0_linear_1_31" x1="453.702" y1="0" x2="453.702" y2="640"
                        gradientUnits="userSpaceOnUse">
                        <stop stop-color="white" />
                        <stop offset="1" stop-color="white" stop-opacity="0" />
                    </linearGradient>
                </defs> -->
            </svg>
            <div class="background-3"></div>
<!--            <div class="background-4"></div>-->
<!--            <div class="background-5c"></div>-->
        </div>
        <div class="top-card">
            <div class="card-stick"></div>
            <h3 class="category-card">Category</h3>
            <h1 class="animal-category">Animals</h1>
            <h3 class="subtitle-card">In this section you have the opportunity
          Buy clothes of different classes</h3>
        </div>
        <img class="animal-dog" src="/img/Shop/dog.png">
        <img class="animal-cat" src="/img/Shop/cat.png">
    </div>
</template>

<script>
export default {
    name: "AnimalsCard"
}
</script>

<style lang="scss" scoped>
* {
  box-sizing: border-box;
}
.animals-card {
    width: 20.394rem;
    height: 18.792rem;
    border: 1.112px solid #171819;
    border-radius: 1.5188rem;
    position: relative;
    overflow: hidden;
    margin-right: 1.5188rem;
    /*transition: transform 0.4s ease;

    &:hover {
        transform: scale(1.04);

    }*/
    &:hover  {
        .background-3{
            opacity: 0.25;
        }
        .animal {
            &-dog {
                transform: translate(2%);
            }
            &-cat {
                transform: translate(-2%);
            }
        }
    }
}

.background {
    position: absolute;
  width: 100%;

    &-1 {
        position: absolute;
        width: 22.95rem;
        height: 21.7125rem;
        top: 0rem;
        left: -1.9688rem;
    }

    &-2 {
        position: absolute;
      width: 22.95rem;
      height: 19.6313rem;
        top: 3.312rem;
        left: -0.1688rem;
    }

    &-3 {
        position: absolute;
        width: 28.2375rem;
        height: 19.0125rem;
        background: linear-gradient(0deg, rgba(224,153,104,1) 0%, rgba(252,70,107,0) 100%);
        top: 0rem;
        opacity: 0.15;
        transition: .3s;
        // filter: blur(5.004rem);
    }

    //&-4 {
    //    background: url("/img/Shop/dog.png");
    //    filter: blur(0px);
    //    opacity: 1;
    //    width: 276.666px;
    //    height: 254.444px;
    //    position: absolute;
    //    top: 212.222px;
    //    left: 0px;
    //}
    //
    //&-5c {
    //    background: url("/img/Shop/cat.png");
    //    background-repeat: no-repeat;
    //    width: 276.666px;
    //    height: 254.444px;
    //    position: absolute;
    //    top: 194.444px;
    //    left: 223.334px;
    //}
}

.top-card {
    display: block;
    padding-top: 1.0688rem;
    padding-left: 1.6875rem;
}

.card-stick {
    width: 1.4625rem;
    height: 0.1125rem;
    background: #FFFFFF;
    border: 0.0563rem solid rgba(255, 255, 255, 0.09);
    box-shadow: 0px 0px 0.7875rem rgba(255, 255, 255, 0.55);
    transform: rotate(-90deg);
    position: absolute;
    top: 2.25rem;
    left: 0px;
}

.category-card {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 0.675rem;
    line-height: 0.7875rem;
    text-transform: uppercase;

    color: rgba(255, 255, 255, 0.55);


}

.animal-category {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 2.7rem;
    line-height: 3.2625rem;
    /* identical to box height */

    text-transform: uppercase;

    color: #FFFFFF;
}

.subtitle-card {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 0.675rem;
    line-height: 0.7875rem;
    text-transform: uppercase;

    color: rgba(255, 255, 255, 0.55);
    max-width: 11.475rem;
}
.animal {
  &-dog {
    // filter: blur(0rem);
    position: absolute;
    width: 12.447rem;
    height: 11.4498rem;
    top: 7.137rem;
    left: 0px;
    transition: 0.3s;
  }
  &-cat {
    position: absolute;
    width: 9.693rem;
    height: 12.447rem;
    top: 6.237rem;
    left: 10.404rem;
    transition: 0.3s;
  }
}
</style>
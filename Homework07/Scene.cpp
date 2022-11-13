//
// Created by Göksu Güvendiren on 2019-05-14.
//

#include "Scene.hpp"


void Scene::buildBVH() {
    printf(" - Generating BVH...\n\n");
    this->bvh = new BVHAccel(objects, 1, BVHAccel::SplitMethod::NAIVE);
}

Intersection Scene::intersect(const Ray &ray) const
{
    return this->bvh->Intersect(ray);
}

void Scene::sampleLight(Intersection &pos, float &pdf) const
{
    float emit_area_sum = 0;
    for (uint32_t k = 0; k < objects.size(); ++k) {
        if (objects[k]->hasEmit()){
            emit_area_sum += objects[k]->getArea();
        }
    }
    float p = get_random_float() * emit_area_sum;
    emit_area_sum = 0;
    for (uint32_t k = 0; k < objects.size(); ++k) {
        if (objects[k]->hasEmit()){
            emit_area_sum += objects[k]->getArea();
            if (p <= emit_area_sum){
                objects[k]->Sample(pos, pdf);
                break;
            }
        }
    }
}

bool Scene::trace(
        const Ray &ray,
        const std::vector<Object*> &objects,
        float &tNear, uint32_t &index, Object **hitObject)
{
    *hitObject = nullptr;
    for (uint32_t k = 0; k < objects.size(); ++k) {
        float tNearK = kInfinity;
        uint32_t indexK;
        Vector2f uvK;
        if (objects[k]->intersect(ray, tNearK, indexK) && tNearK < tNear) {
            *hitObject = objects[k];
            tNear = tNearK;
            index = indexK;
        }
    }


    return (*hitObject != nullptr);
}

// Implementation of Path Tracing
Vector3f Scene::castRay(const Ray& ray, int depth) const
{
    // TO DO Implement Path Tracing Algorithm here
    
    Intersection inter = intersect(ray);

    if (!inter.happened)
        return Vector3f(0, 0, 0);

    if (inter.m->hasEmission()) {
        if (depth == 0)
            return inter.m->getEmission();
        else
            return Vector3f(0, 0, 0);
    }

    Vector3f& p = inter.coords;
    Material*& material = inter.m;
    Vector3f normal = normalize(inter.normal);
    Vector3f wo = normalize(-ray.direction);

    auto format = [](Vector3f& a) {
        if (a.x < 0) a.x = 0;
        if (a.y < 0) a.y = 0;
        if (a.z < 0) a.z = 0;
    };

    Vector3f L_dir(0, 0, 0);
    {
        Intersection lightInter;
        float pdf_light = 0.0f;
        sampleLight(lightInter, pdf_light);

        Vector3f light_normal = normalize(lightInter.normal);
        Vector3f& x = lightInter.coords;
        Vector3f ws = normalize(x - p);

        Intersection pwsInter = intersect(Ray(p,ws));

        if (pwsInter.happened && (pwsInter.coords - x).norm() < 1e-2)
        {
            L_dir = lightInter.emit * material->eval(ws, wo, normal) * dotProduct(ws,normal) * dotProduct(-ws, light_normal)
                / (dotProduct((x-p),(x-p)) * pdf_light);
            format(L_dir);
        }
    }
    Vector3f L_indir(0, 0, 0);
    {
        if (get_random_float() < RussianRoulette)
        {
            Vector3f wi = material->sample(wo, normal).normalized();

            Ray nextRay(p, wi);
            Intersection nextInter = intersect(nextRay);
            if (nextInter.happened && !nextInter.m->hasEmission())
            {
                L_indir = castRay(nextRay, depth + 1) * material->eval(wi, wo, normal) * dotProduct(wi, normal)
                    / (material->pdf(wi, wo, normal) * RussianRoulette);
                format(L_indir);
            }
            
        }
    }
    return L_dir + L_indir;
}
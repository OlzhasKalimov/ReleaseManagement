﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ReleaseManagement.Core.Models;
using ReleaseManagement.Data.Context;

namespace ReleaseManagement.Controllers {
    public class FeaturesController : ApiController {
        private ReleaseManagementContext db = new ReleaseManagementContext();

        // GET: api/Features
        public IHttpActionResult GetFeatures() {
            var features = db.Features.OrderBy(p => p.DisplayName).ToList();
            return Ok(features);
        }

        // GET: api/Features/5
        [ResponseType(typeof(Feature))]
        public IHttpActionResult GetFeature(int id) {
            Feature feature = db.Features.Find(id);
            if (feature == null) {
                return NotFound();
            }

            return Ok(feature);
        }

        // PUT: api/Features/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFeature(int id, Feature feature) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != feature.FeatureId) {
                return BadRequest();
            }

            db.Entry(feature).State = EntityState.Modified;

            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException) {
                if (!FeatureExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Features
        [ResponseType(typeof(Feature))]
        public IHttpActionResult PostFeature(Feature feature) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            db.Features.Add(feature);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = feature.FeatureId }, feature);
        }

        
        // DELETE: api/Features/5
        [ResponseType(typeof(Feature))]
        public IHttpActionResult DeleteFeature(int id) {
            Feature feature = db.Features.Find(id);
            if (feature == null) {
                return NotFound();
            }

            db.Features.Remove(feature);
            db.SaveChanges();

            return Ok(feature);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeatureExists(int id) {
            return db.Features.Count(e => e.FeatureId == id) > 0;
        }
    }
}